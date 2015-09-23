using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Daos;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Daos.Tests
{
    [TestClass()]
    public class LoanDAOTests
    {
        private TestContext testContextInstance;
        private LoanDAO _loanDao;
        private ILoanHelper _helper;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void Initialize()
        {
            _helper = new LoanHelper();
            _loanDao = new LoanDAO(_helper);
        }

        [TestMethod()]
        public void LoanListTest()
        {
            List<ILoan> expected = new List<ILoan>();
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _loanDao.LoanList, "Library.Daos.LoanDAO.LoanList property test failed");
        }

        [TestMethod()]
        public void CommitLoanTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            ILoan loan = null;

            _loanDao.CommitLoan(loan);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.CommitLoan Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void CreateLoanTest()
        {
            DateTime methodStartTime = DateTime.Now;
            ILoan expected = null;

            //Parameters
            IMember borrower = null;
            IBook book = null;
            DateTime borrowDate = new DateTime(1969, 7, 21);
            DateTime dueDate = new DateTime(1969, 7, 21);

            ILoan results = _loanDao.CreateLoan(borrower, book, borrowDate, dueDate);
            Assert.AreEqual(expected, results, "Library.Daos.LoanDAO.CreateLoan method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.CreateLoan Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindLoansByBookTitleTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<ILoan> expected = new List<ILoan>();

            //Parameters
            string title = "test";

            List<ILoan> results = _loanDao.FindLoansByBookTitle(title);
            Assert.AreEqual(expected, results, "Library.Daos.LoanDAO.FindLoansByBookTitle method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.FindLoansByBookTitle Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindLoansByBorrowerTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<ILoan> expected = new List<ILoan>();

            //Parameters
            IMember borrower = null;

            List<ILoan> results = _loanDao.FindLoansByBorrower(borrower);
            Assert.AreEqual(expected, results, "Library.Daos.LoanDAO.FindLoansByBorrower method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.FindLoansByBorrower Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindOverDueLoansTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<ILoan> expected = new List<ILoan>();

            //Parameters

            List<ILoan> results = _loanDao.FindOverDueLoans();
            Assert.AreEqual(expected, results, "Library.Daos.LoanDAO.FindOverDueLoans method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.FindOverDueLoans Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void GetLoanByBookTest()
        {
            DateTime methodStartTime = DateTime.Now;
            ILoan expected = null;

            //Parameters
            IBook book = null;

            ILoan results = _loanDao.GetLoanByBook(book);
            Assert.AreEqual(expected, results, "Library.Daos.LoanDAO.GetLoanByBook method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.GetLoanByBook Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void GetLoanByIDTest()
        {
            DateTime methodStartTime = DateTime.Now;
            ILoan expected = null;

            //Parameters
            int id = 123;

            ILoan results = _loanDao.GetLoanByID(id);
            Assert.AreEqual(expected, results, "Library.Daos.LoanDAO.GetLoanByID method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.GetLoanByID Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void UpdateOverDueStatusTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            DateTime currentDate = new DateTime(1969, 7, 21);

            _loanDao.UpdateOverDueStatus(currentDate);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.LoanDAO.UpdateOverDueStatus Time Elapsed: {0}", methodDuration));
        }
    }
}