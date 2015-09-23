using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Interfaces.Entities;

namespace Library.Entities.Tests
{
    [TestClass()]
    public class LoanTests
    {
        private TestContext testContextInstance;
        private Loan _loan;

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
            IBook book = null;
            IMember borrower = null;
            DateTime borrowDate = new DateTime(1969, 7, 21);
            DateTime returnDate = new DateTime(1969, 7, 21);
            _loan = new Loan(book,borrower, borrowDate, returnDate);
        }

        [TestMethod()]
        public void BookTest()
        {
            IBook expected = null;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _loan.Book, "Library.Entities.Loan.Book property test failed");
        }

        [TestMethod()]
        public void BorrowerTest()
        {
            IMember expected = null;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _loan.Borrower, "Library.Entities.Loan.Borrower property test failed");
        }

        [TestMethod()]
        public void IDTest()
        {
            int expected = 123;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _loan.ID, "Library.Entities.Loan.ID property test failed");
        }

        [TestMethod()]
        public void IsOverDueTest()
        {
            bool expected = true;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _loan.IsOverDue, "Library.Entities.Loan.IsOverDue property test failed");
        }

        [TestMethod()]
        public void StateTest()
        {
            LoanState expected = new LoanState();
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _loan.State, "Library.Entities.Loan.State property test failed");
        }

        [TestMethod()]
        public void CheckOverDueTest()
        {
            DateTime methodStartTime = DateTime.Now;
            bool expected = true;

            //Parameters
            DateTime currentDate = new DateTime(1969, 7, 21);

            bool results = _loan.CheckOverDue(currentDate);
            Assert.AreEqual(expected, results, "Library.Entities.Loan.CheckOverDue method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Loan.CheckOverDue Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void CommitTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            int id = 123;

            _loan.Commit(id);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Loan.Commit Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void CompleteTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters

            _loan.Complete();

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Loan.Complete Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            DateTime methodStartTime = DateTime.Now;
            string expected = "test";

            //Parameters

            string results = _loan.ToString();
            Assert.AreEqual(expected, results, "Library.Entities.Loan.ToString method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Loan.ToString Time Elapsed: {0}", methodDuration));
        }
    }
}