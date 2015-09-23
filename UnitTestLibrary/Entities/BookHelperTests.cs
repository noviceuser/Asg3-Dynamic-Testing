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
    public class BookHelperTests
    {
        private TestContext testContextInstance;
        private BookHelper _bookHelper;

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

        public void Initialize()
        {
            //New instance of Book Helper
            _bookHelper = new BookHelper();
        }

        [TestMethod()]
        public void MakeBookTest()
        {
            DateTime methodStartTime = DateTime.Now;
            IBook expected = null;

            //Parameters
            string author = "test";
            string title = "test";
            string callNumber = "test";
            int id = 123;

            IBook results = _bookHelper.MakeBook(author, title, callNumber, id);
            Assert.AreEqual(expected, results, "Library.Entities.BookHelper.MakeBook method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.BookHelper.MakeBook Time Elapsed: {0}", methodDuration));
        }
    }
}