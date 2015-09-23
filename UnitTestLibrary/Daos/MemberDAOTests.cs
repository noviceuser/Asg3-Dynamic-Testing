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
    public class MemberDAOTests
    {
        private TestContext testContextInstance;
        private MemberDAO _memberDao;
        private IMemberHelper _helper;

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
            _helper = new MemberHelper();
            _memberDao = new MemberDAO(_helper);
        }

        [TestMethod()]
        public void MemberListTest()
        {
            List<IMember> expected = new List<IMember>();
            //TODO: Read Only Property, Delete this line and change the expected value
            Assert.AreEqual(expected, _memberDao.MemberList, "Library.Daos.MemberDAO.MemberList property test failed");
        }

        [TestMethod()]
        public void AddMemberTest()
        {
            DateTime methodStartTime = DateTime.Now;
            IMember expected = null;

            //Parameters
            string firstName = "test";
            string lastName = "test";
            string contactPhone = "test";
            string emailAddress = "test";

            IMember results = _memberDao.AddMember(firstName, lastName, contactPhone, emailAddress);
            Assert.AreEqual(expected, results, "Library.Daos.MemberDAO.AddMember method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.MemberDAO.AddMember Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindMembersByEmailAddressTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<IMember> expected = new List<IMember>();

            //Parameters
            string emailAddress = "test";

            List<IMember> results = _memberDao.FindMembersByEmailAddress(emailAddress);
            Assert.AreEqual(expected, results, "Library.Daos.MemberDAO.FindMembersByEmailAddress method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.MemberDAO.FindMembersByEmailAddress Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindMembersByLastNameTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<IMember> expected = new List<IMember>();

            //Parameters
            string lastName = "test";

            List<IMember> results = _memberDao.FindMembersByLastName(lastName);
            Assert.AreEqual(expected, results, "Library.Daos.MemberDAO.FindMembersByLastName method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.MemberDAO.FindMembersByLastName Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void FindMembersByNamesTest()
        {
            DateTime methodStartTime = DateTime.Now;
            List<IMember> expected = new List<IMember>();

            //Parameters
            string firstName = "test";
            string lastName = "test";

            List<IMember> results = _memberDao.FindMembersByNames(firstName, lastName);
            Assert.AreEqual(expected, results, "Library.Daos.MemberDAO.FindMembersByNames method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.MemberDAO.FindMembersByNames Time Elapsed: {0}", methodDuration));
        }

        [TestMethod()]
        public void GetMemberByIDTest()
        {
            DateTime methodStartTime = DateTime.Now;
            IMember expected = null;

            //Parameters
            int id = 123;

            IMember results = _memberDao.GetMemberByID(id);
            Assert.AreEqual(expected, results, "Library.Daos.MemberDAO.GetMemberByID method test failed");

            TimeSpan methodDuration = DateTime.Now.Subtract(methodStartTime);
            Console.WriteLine(String.Format("Library.Daos.MemberDAO.GetMemberByID Time Elapsed: {0}", methodDuration));
        }
    }
}