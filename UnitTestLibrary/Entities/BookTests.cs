using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library.Entities;
using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities.Tests
{
    [TestClass()]
    public class BookTests
    {
        private TestContext testContextInstance;
        private Book _book;

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
            //New instance of Book
            _book = new Book("test", "test", "test", 123);
        }
        [TestMethod()]
        public void AuthorTest()
        {
            string expected = "test";
            Assert.AreEqual(expected, _book.Author, "Library.Entities.Book.Author property test failed");
        }

        [TestMethod()]
        public void CallNumberTest()
        {
            string expected = "test";
            Assert.AreEqual(expected, _book.CallNumber, "Library.Entities.Book.CallNumber property test failed");
        }

        [TestMethod()]
        public void IDTest()
        {
            int expected = 123;
            Assert.AreEqual(expected, _book.ID, "Library.Entities.Book.ID property test failed");
        }

        [TestMethod()]
        public void LoanTest()
        {
            ILoan expected = null;
            Assert.AreEqual(expected, _book.Loan, "Library.Entities.Book.Loan property test failed");
        }

        [TestMethod()]
        public void StateTest()
        {
            BookState expected = new BookState();
            Assert.AreEqual(expected, _book.State, "Library.Entities.Book.State property test failed");
        }

        [TestMethod()]
        public void TitleTest()
        {
            string expected = "test";
            Assert.AreEqual(expected, _book.Title, "Library.Entities.Book.Title property test failed");
        }

        [TestMethod()]
        public void BorrowTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            ILoan loan = null;

            _book.Borrow(loan);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Book.Borrow Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void DisposeTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters

            _book.Dispose();

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Book.Dispose Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void LoseTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters

            _book.Lose();

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Book.Lose Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void RepairTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters

            _book.Repair();

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Book.Repair Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void ReturnBookTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            bool damaged = true;

            _book.ReturnBook(damaged);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Book.ReturnBook Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            DateTime methodStartTime = DateTime.Now;
            string expected = "test";

            //Parameters

            string results = _book.ToString();
            Assert.AreEqual(expected, results, "Library.Entities.Book.ToString method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Book.ToString Time Elapsed: {0}", methodDuration));
        }
    }
}