using Library.Controllers;
using Library.Daos;
using Library.Entities;
using Library.Hardware;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Interfaces.Hardware;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Library
{
    public partial class MainWindow : Window, IDisplay
    {
        private UserControl _currentControl;

        private CardReader _reader;

        private Scanner _scanner;

        private Printer _printer;

        private IBookDAO _bookDAO;

        private ILoanDAO _loanDAO;

        private IMemberDAO _memberDAO;

        public UserControl Display
        {
            get
            {
                Console.WriteLine("Getting Display");
                return _currentControl;
            }
            set
            {
                Console.WriteLine(string.Concat("Setting Display : ", value));
                Panel.Children.Remove(_currentControl);
                _currentControl = value;
                Panel.Children.Add(_currentControl);
            }
        }

        public MainWindow()
        {
            _reader = new CardReader();
            _scanner = new Scanner();
            _printer = new Printer();
            InitializeComponent();
            _reader.Show();
            _scanner.Show();
            _printer.Show();
            _bookDAO = new BookDAO(new BookHelper());
            _loanDAO = new LoanDAO(new LoanHelper());
            _memberDAO = new MemberDAO(new MemberHelper());
            SetUpTestData();
            (new MainMenuController(this, _reader, _scanner, _printer, _bookDAO, _loanDAO, _memberDAO)).initialise();
        }

        private void SetUpTestData()
        {
            IBook[] bookArray = new IBook[15];
            IMember[] memberArray = new IMember[6];
            bookArray[0] = _bookDAO.AddBook("author1", "title1", "callNo1");
            bookArray[1] = _bookDAO.AddBook("author1", "title2", "callNo2");
            bookArray[2] = _bookDAO.AddBook("author1", "title3", "callNo3");
            bookArray[3] = _bookDAO.AddBook("author1", "title4", "callNo4");
            bookArray[4] = _bookDAO.AddBook("author2", "title5", "callNo5");
            bookArray[5] = _bookDAO.AddBook("author2", "title6", "callNo6");
            bookArray[6] = _bookDAO.AddBook("author2", "title7", "callNo7");
            bookArray[7] = _bookDAO.AddBook("author2", "title8", "callNo8");
            bookArray[8] = _bookDAO.AddBook("author3", "title9", "callNo9");
            bookArray[9] = _bookDAO.AddBook("author3", "title10", "callNo10");
            bookArray[10] = _bookDAO.AddBook("author4", "title11", "callNo11");
            bookArray[11] = _bookDAO.AddBook("author4", "title12", "callNo12");
            bookArray[12] = _bookDAO.AddBook("author5", "title13", "callNo13");
            bookArray[13] = _bookDAO.AddBook("author5", "title14", "callNo14");
            bookArray[14] = _bookDAO.AddBook("author5", "title15", "callNo15");
            memberArray[0] = _memberDAO.AddMember("fName1", "lName1", "0001", "email1");
            memberArray[1] = _memberDAO.AddMember("fName2", "lName2", "0002", "email2");
            memberArray[2] = _memberDAO.AddMember("fName3", "lName3", "0003", "email3");
            memberArray[3] = _memberDAO.AddMember("fName4", "lName4", "0004", "email4");
            memberArray[4] = _memberDAO.AddMember("fName5", "lName5", "0005", "email5");
            memberArray[5] = _memberDAO.AddMember("fName6", "lName6", "0006", "email6");
            DateTime now = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(14, 0, 0, 0);
            DateTime dateTime = now.Add(timeSpan);
            for (int i = 0; i < 2; i++)
            {
                ILoan loan = _loanDAO.CreateLoan(memberArray[1], bookArray[i], now, dateTime);
                _loanDAO.CommitLoan(loan);
            }
            DateTime dateTime1 = dateTime.Add(new TimeSpan(1, 0, 0, 0));
            _loanDAO.UpdateOverDueStatus(dateTime1);
            memberArray[2].AddFine(10f);
            for (int j = 2; j < 7; j++)
            {
                ILoan loan1 = _loanDAO.CreateLoan(memberArray[3], bookArray[j], now, dateTime);
                _loanDAO.CommitLoan(loan1);
            }
            memberArray[4].AddFine(5f);
            for (int k = 7; k < 9; k++)
            {
                ILoan loan2 = _loanDAO.CreateLoan(memberArray[5], bookArray[k], now, dateTime);
                _loanDAO.CommitLoan(loan2);
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("detected Window closing");
            Application.Current.Shutdown();
        }
    }
}