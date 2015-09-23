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
    public class LoanHelperTests
    {
        private TestContext testContextInstance;
        private LoanHelper _loanHelper;

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
            //New instance of Loan Helper
            _loanHelper = new LoanHelper();
        }

        [TestMethod()]
        public void MakeLoanTest()
        {
            DateTime methodStartTime = DateTime.Now;
            ILoan expected = null;

            //Parameters
            IBook book = null;
            IMember borrower = null;
            DateTime borrowDate = new DateTime(1969, 7, 21);
            DateTime dueDate = new DateTime(1969, 7, 21);

            ILoan results = _loanHelper.MakeLoan(book, borrower, borrowDate, dueDate);
            Assert.AreEqual(expected, results, "Library.Entities.LoanHelper.MakeLoan method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.LoanHelper.MakeLoan Time Elapsed: {0}", methodDuration));
        }
    }
}