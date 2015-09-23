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
    public class MemberTests
    {
        private TestContext testContextInstance;
        private Member _member;

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
            //New instance of Member
            _member = new Member("test","test","test","test",123);
        }

        [TestMethod()]
        public void ContactPhoneTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.ContactPhone, "Library.Entities.Member.ContactPhone property test failed");
        }

        [TestMethod()]
        public void EmailAddressTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.EmailAddress, "Library.Entities.Member.EmailAddress property test failed");
        }

        [TestMethod()]
        public void FineAmountTest()
        {
            float expected = 2.99999F;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.FineAmount, "Library.Entities.Member.FineAmount property test failed");
        }

        [TestMethod()]
        public void FirstNameTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.FirstName, "Library.Entities.Member.FirstName property test failed");
        }

        [TestMethod()]
        public void HasFinesPayableTest()
        {
            bool expected = true;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.HasFinesPayable, "Library.Entities.Member.HasFinesPayable property test failed");
        }

        [TestMethod()]
        public void HasOverDueLoansTest()
        {
            bool expected = true;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.HasOverDueLoans, "Library.Entities.Member.HasOverDueLoans property test failed");
        }

        [TestMethod()]
        public void HasReachedFineLimitTest()
        {
            bool expected = true;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.HasReachedFineLimit, "Library.Entities.Member.HasReachedFineLimit property test failed");
        }

        [TestMethod()]
        public void HasReachedLoanLimitTest()
        {
            bool expected = true;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.HasReachedLoanLimit, "Library.Entities.Member.HasReachedLoanLimit property test failed");
        }

        [TestMethod()]
        public void IDTest()
        {
            int expected = 123;
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.ID, "Library.Entities.Member.ID property test failed");
        }

        [TestMethod()]
        public void LastNameTest()
        {
            string expected = "test";
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.LastName, "Library.Entities.Member.LastName property test failed");
        }

        [TestMethod()]
        public void LoansTest()
        {
            List<ILoan> expected = new List<ILoan>();
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.Loans, "Library.Entities.Member.Loans property test failed");
        }

        [TestMethod()]
        public void StateTest()
        {
            MemberState expected = new MemberState();
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _member.State, "Library.Entities.Member.State property test failed");
        }

        [TestMethod()]
        public void AddFineTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            float fine = 2.99999F;

            _member.AddFine(fine);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Member.AddFine Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void AddLoanTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            ILoan loan = null;

            _member.AddLoan(loan);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Member.AddLoan Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void PayFineTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            float payment = 2.99999F;

            _member.PayFine(payment);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Member.PayFine Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void RemoveLoanTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters
            ILoan loan = null;

            _member.RemoveLoan(loan);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Member.RemoveLoan Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void ToStringTest()
        {
            DateTime methodStartTime = DateTime.Now;
            string expected = "test";

            //Parameters

            string results = _member.ToString();
            Assert.AreEqual(expected, results, "Library.Entities.Member.ToString method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Member.ToString Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void updateStateTest()
        {
            DateTime methodStartTime = DateTime.Now;

            //Parameters

            System.Reflection.MethodInfo method =
                typeof(Member).GetMethod("updateState",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Member member = new Member("test", "test", "test", "test", 123);
            method.Invoke(member, null);

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.Member.updateState Time Elapsed: {0}", methodDuration));
        }
    }
}