using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace Library.Daos
{
    public class LoanDAO : ILoanDAO
    {
        private ILoanHelper helper;

        private Dictionary<int, ILoan> loanDict;

        private int _nextID;

        public List<ILoan> LoanList
        {
            get
            {
                List<ILoan> loans = new List<ILoan>();
                foreach (ILoan value in loanDict.Values)
                {
                    loans.Add(value);
                }
                return loans;
            }
        }

        private int NextID
        {
            get
            {
                int num = _nextID;
                _nextID = num + 1;
                return num;
            }
            set
            {
                _nextID = value;
            }
        }

        public LoanDAO(ILoanHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentException(string.Format("LoanDAO : constructor : helper cannot be null.", new object[0]));
            }
            this.helper = helper;
            loanDict = new Dictionary<int, ILoan>();
            NextID = 1;
        }

        public void CommitLoan(ILoan loan)
        {
            if (loan == null)
            {
                throw new ArgumentException(string.Format("LoanDAO : commitLoans : loan cannot be null.", new object[0]));
            }
            loan.Commit(NextID);
            loanDict.Add(loan.ID, loan);
        }

        public ILoan CreateLoan(IMember borrower, IBook book, DateTime borrowDate, DateTime dueDate)
        {
            if (borrower == null || book == null)
            {
                throw new ArgumentException(string.Format("LoanMapDAO : CreatePendingLoan : parameters cannot be null.", new object[0]));
            }
            if (DateTime.Compare(borrowDate, dueDate) > 0)
            {
                throw new ArgumentException(string.Format("LoanDAO : createPendingLoan : borrowDate cannot be after dueDate.", new object[0]));
            }
            return helper.MakeLoan(book, borrower, borrowDate, dueDate);
        }

        public List<ILoan> FindLoansByBookTitle(string title)
        {
            List<ILoan> loans = new List<ILoan>();
            foreach (ILoan value in loanDict.Values)
            {
                if (!value.Book.Title.Equals(title))
                {
                    continue;
                }
                loans.Add(value);
            }
            return loans;
        }

        public List<ILoan> FindLoansByBorrower(IMember borrower)
        {
            List<ILoan> loans = new List<ILoan>();
            foreach (ILoan value in loanDict.Values)
            {
                if (!value.Borrower.Equals(borrower))
                {
                    continue;
                }
                loans.Add(value);
            }
            return loans;
        }

        public List<ILoan> FindOverDueLoans()
        {
            List<ILoan> loans = new List<ILoan>();
            foreach (ILoan value in loanDict.Values)
            {
                if (!value.IsOverDue)
                {
                    continue;
                }
                loans.Add(value);
            }
            return loans;
        }

        public ILoan GetLoanByBook(IBook book)
        {
            ILoan loan;
            if (book == null)
            {
                throw new ArgumentException(string.Format("LoanMapDAO : getLoanByBook : book cannot be null.", new object[0]));
            }
            Dictionary<int, ILoan>.ValueCollection.Enumerator enumerator = loanDict.Values.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    ILoan current = enumerator.Current;
                    if (!book.Equals(current.Book))
                    {
                        continue;
                    }
                    loan = current;
                    return loan;
                }
                return null;
            }
            finally
            {
                ((IDisposable)enumerator).Dispose();
            }
        }

        public ILoan GetLoanByID(int id)
        {
            if (!loanDict.ContainsKey(id))
            {
                return null;
            }
            return loanDict[id];
        }

        public void UpdateOverDueStatus(DateTime currentDate)
        {
            foreach (ILoan value in loanDict.Values)
            {
                value.CheckOverDue(currentDate);
            }
        }
    }
}