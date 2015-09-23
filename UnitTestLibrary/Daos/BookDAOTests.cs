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
    public class BookDAOTests
    {
        private TestContext testContextInstance;
        private BookDAO _bookDao;
        private IBookHelper _helper;

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
            _helper = new BookHelper();
            _bookDao = new BookDAO(_helper);
        }

        public void BookListTest()
        {
            List<IBook> expected = new List<IBook>();
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _bookDao.BookList, "Library.Daos.BookDAO.BookList property test failed");
        }

        [TestMethod()]
        public void AddBookTest()
        {
            DateTime methodStartTime = DateTime.Now;
            IBook expected = null;

            //Parameters
            string author = "test";
            string title = "test";
            string callNo = "test";

            IBook results = _bookDao.AddBook(author, title, callNo);
            Assert.AreEqual(expected, results, "Library.Daos.BookDAO.AddBook method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.BookDAO.AddBook Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindBooksByAuthorTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<IBook> expected = new List<IBook>();

            //Parameters
            string author = "test";

            List<IBook> results = _bookDao.FindBooksByAuthor(author);
            Assert.AreEqual(expected, results, "Library.Daos.BookDAO.FindBooksByAuthor method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.BookDAO.FindBooksByAuthor Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindBooksByAuthorTitleTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<IBook> expected = new List<IBook>();

            //Parameters
            string author = "test";
            string title = "test";

            List<IBook> results = _bookDao.FindBooksByAuthorTitle(author, title);
            Assert.AreEqual(expected, results, "Library.Daos.BookDAO.FindBooksByAuthorTitle method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.BookDAO.FindBooksByAuthorTitle Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindBooksByTitleTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<IBook> expected = new List<IBook>();

            //Parameters
            string title = "test";

            List<IBook> results = _bookDao.FindBooksByTitle(title);
            Assert.AreEqual(expected, results, "Library.Daos.BookDAO.FindBooksByTitle method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.BookDAO.FindBooksByTitle Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void GetBookByIDTest()
        {
            DateTime methodStartTime = DateTime.Now;
            IBook expected = null;

            //Parameters
            int id = 123;

            IBook results = _bookDao.GetBookByID(id);
            Assert.AreEqual(expected, results, "Library.Daos.BookDAO.GetBookByID method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.BookDAO.GetBookByID Time Elapsed: {0}", methodDuration));
        }
    }
}