using Library.Controls.Borrow;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Library.Controllers.Borrow
{
    internal class BorrowController : IBorrowListener, ICardReaderListener, IScannerListener
    {
        private IDisplay _display;

        private UserControl _previousDisplay;

        private ABorrowControl _ui;

        private ICardReader _reader;

        private ICardReaderListener _previousReaderListener;

        private IScanner _scanner;

        private IScannerListener _previousScannerListener;

        private IPrinter _printer;

        private IBookDAO _bookDAO;

        private ILoanDAO _loanDAO;

        private IMemberDAO _memberDAO;

        private IMember _borrower;

        private int scanCount;

        private EBorrowState _state;

        private List<IBook> _bookList;

        private List<ILoan> _loanList;

        public BorrowController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer, IBookDAO bookDAO, ILoanDAO loanDAO, IMemberDAO memberDAO)
        {
            _display = display;
            _reader = reader;
            _scanner = scanner;
            _printer = printer;
            _bookDAO = bookDAO;
            _loanDAO = loanDAO;
            _memberDAO = memberDAO;
            _ui = new BorrowControl(this);
            _state = EBorrowState.CREATED;
        }

        public void bookScanned(int barcode)
        {
            Console.WriteLine(string.Concat("bookScanned: got ", barcode));
            if (_state != EBorrowState.SCANNING_BOOKS)
            {
                throw new ApplicationException(string.Format("BorrowUC_CTL : bookScanned : illegal operation in state: {0}", _state));
            }
            _ui.DisplayErrorMessage("");
            IBook bookByID = _bookDAO.GetBookByID(barcode);
            if (bookByID == null)
            {
                _ui.DisplayErrorMessage(string.Format("Book {0} not found", barcode));
                return;
            }
            if (bookByID.State != BookState.AVAILABLE)
            {
                _ui.DisplayErrorMessage(string.Format("Book {0} is not available: {1}", bookByID.ID, bookByID.State));
                return;
            }
            if (_bookList.Contains(bookByID))
            {
                _ui.DisplayErrorMessage(string.Format("Book {0} already scanned: ", bookByID.ID));
                return;
            }
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(14, 0, 0, 0);
            DateTime dateTime = now.Add(timeSpan);
            ILoan loan = _loanDAO.CreateLoan(_borrower, bookByID, now, dateTime);
            scanCount = scanCount + 1;
            _bookList.Add(bookByID);
            _loanList.Add(loan);
            Console.WriteLine("scancount = {0}", scanCount);
            _ui.DisplayScannedBookDetails(bookByID.ToString());
            _ui.DisplayPendingLoan(buildLoanListDisplay(_loanList));
            if (scanCount >= 5)
            {
                setState(EBorrowState.CONFIRMING_LOANS);
            }
        }

        private string buildLoanListDisplay(List<ILoan> loanList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ILoan loan in loanList)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append("\n\n");
                }
                stringBuilder.Append(loan.ToString());
            }
            return stringBuilder.ToString();
        }

        public void cancelled()
        {
            setState(EBorrowState.CANCELLED);
        }

        public void cardSwiped(int memberID)
        {
            Console.WriteLine("detected card swipe: {0}", memberID);
            _scanner.Enabled = true;
            _reader.Enabled = false;
            if (_state != EBorrowState.INITIALIZED)
            {
                throw new ApplicationException(string.Format("BorrowUC_CTL : cardSwiped : illegal operation in state: {0}", _state));
            }
            _borrower = _memberDAO.GetMemberByID(memberID);
            if (_borrower == null)
            {
                _ui.DisplayErrorMessage(string.Format("Member ID {0} not found", memberID));
                _reader.Enabled = true;
                return;
            }
            bool hasOverDueLoans = _borrower.HasOverDueLoans;
            bool hasReachedLoanLimit = _borrower.HasReachedLoanLimit;
            bool hasFinesPayable = _borrower.HasFinesPayable;
            bool hasReachedFineLimit = _borrower.HasReachedFineLimit;
            if (!(hasOverDueLoans | hasReachedLoanLimit | hasReachedFineLimit))
            {
                setState(EBorrowState.SCANNING_BOOKS);
            }
            else
            {
                setState(EBorrowState.BORROWING_RESTRICTED);
            }
            int d = _borrower.ID;
            string str = string.Concat(_borrower.FirstName, " ", _borrower.LastName);
            string contactPhone = _borrower.ContactPhone;
            _ui.DisplayMemberDetails(d, str, contactPhone);
            if (hasOverDueLoans)
            {
                _ui.DisplayOverDueMessage();
            }
            if (hasReachedLoanLimit)
            {
                _ui.DisplayAtLoanLimitMessage();
            }
            if (hasFinesPayable)
            {
                float fineAmount = _borrower.FineAmount;
                _ui.DisplayOutstandingFineMessage(fineAmount);
            }
            if (hasReachedFineLimit)
            {
                Console.WriteLine(string.Concat("State: ", _state));
                float single = _borrower.FineAmount;
                _ui.DisplayOverFineLimitMessage(single);
            }
            foreach (ILoan loan in _borrower.Loans)
            {
                _ui.DisplayExistingLoan(loan.ToString());
            }
        }

        public void close()
        {
            _display.Display = _previousDisplay;
            _reader.Listener = _previousReaderListener;
            _scanner.Listener = _previousScannerListener;
        }

        public void initialise()
        {
            Console.WriteLine("BorrowController Initialising");
            _previousReaderListener = _reader.Listener;
            _previousScannerListener = _scanner.Listener;
            _previousDisplay = _display.Display;
            Console.WriteLine(string.Concat("BorrowController Initialising, previous display = ", _previousDisplay));
            _display.Display = _ui;
            setState(EBorrowState.INITIALIZED);
        }

        public void loansConfirmed()
        {
            setState(EBorrowState.COMPLETED);
        }

        public void loansRejected()
        {
            setState(EBorrowState.SCANNING_BOOKS);
        }

        public void scansCompleted()
        {
            Console.WriteLine("detected scans completed");
            setState(EBorrowState.CONFIRMING_LOANS);
        }

        private void setState(EBorrowState state)
        {
            Console.WriteLine(string.Concat("Setting state: ", state));
            _state = state;
            _ui.State = state;
            switch (state)
            {
                case EBorrowState.INITIALIZED:
                    {
                        _reader.Listener = this;
                        _scanner.Listener = this;
                        _reader.Enabled = true;
                        _scanner.Enabled = false;
                        return;
                    }
                case EBorrowState.SCANNING_BOOKS:
                    {
                        _reader.Enabled = false;
                        _scanner.Enabled = true;
                        _bookList = new List<IBook>();
                        _loanList = new List<ILoan>();
                        scanCount = _borrower.Loans.Count;
                        _ui.DisplayScannedBookDetails("");
                        _ui.DisplayPendingLoan("");
                        return;
                    }
                case EBorrowState.CONFIRMING_LOANS:
                    {
                        _reader.Enabled = false;
                        _scanner.Enabled = false;
                        _ui.DisplayConfirmingLoan(buildLoanListDisplay(_loanList));
                        return;
                    }
                case EBorrowState.COMPLETED:
                    {
                        _reader.Enabled = false;
                        _scanner.Enabled = false;
                        foreach (ILoan loan in _loanList)
                        {
                            _loanDAO.CommitLoan(loan);
                        }
                        _printer.print(buildLoanListDisplay(_loanList));
                        close();
                        return;
                    }
                case EBorrowState.BORROWING_RESTRICTED:
                    {
                        _reader.Enabled = false;
                        _scanner.Enabled = false;
                        _ui.DisplayErrorMessage(string.Format("Member {0} cannot borrow at this time.", _borrower.ID));
                        return;
                    }
                case EBorrowState.CANCELLED:
                    {
                        _reader.Enabled = false;
                        _scanner.Enabled = false;
                        close();
                        return;
                    }
            }
            throw new ApplicationException("Unknown state");
        }
    }
}