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
    public class MemberHelperTests
    {
        private TestContext testContextInstance;
        private MemberHelper _memberHelper;

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
            //New instance of Member Helper
            _memberHelper = new MemberHelper();
        }

        [TestMethod()]
        public void MakeMemberTest()
        {
            DateTime methodStartTime = DateTime.Now;
            IMember expected = null;

            //Parameters
            string firstName = "test";
            string lastName = "test";
            string contactPhone = "test";
            string emailAddress = "test";
            int id = 123;

            IMember results = _memberHelper.MakeMember(firstName, lastName, contactPhone, emailAddress, id);
            Assert.AreEqual(expected, results, "Library.Entities.MemberHelper.MakeMember method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Entities.MemberHelper.MakeMember Time Elapsed: {0}", methodDuration));
        }
    }
}