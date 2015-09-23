using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace Library.Daos
{
    public class MemberDAO : IMemberDAO
    {
        private IMemberHelper helper;

        private Dictionary<int, IMember> memberDict;

        private int nextID;

        public List<IMember> MemberList
        {
            get
            {
                List<IMember> members = new List<IMember>();
                foreach (IMember value in memberDict.Values)
                {
                    members.Add(value);
                }
                return members;
            }
        }

        private int NextID
        {
            get
            {
                int num = nextID;
                nextID = num + 1;
                return num;
            }
        }

        public MemberDAO(IMemberHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentException(string.Format("MemberDAO : constructor : helper cannot be null.", new object[0]));
            }
            helper = helper;
            memberDict = new Dictionary<int, IMember>();
            nextID = 1;
        }

        public IMember AddMember(string firstName, string lastName, string contactPhone, string emailAddress)
        {
            int nextID = NextID;
            IMember member = helper.MakeMember(firstName, lastName, contactPhone, emailAddress, nextID);
            memberDict.Add(nextID, member);
            return member;
        }

        public List<IMember> FindMembersByEmailAddress(string emailAddress)
        {
            return null;
        }

        public List<IMember> FindMembersByLastName(string lastName)
        {
            return null;
        }

        public List<IMember> FindMembersByNames(string firstName, string lastName)
        {
            return null;
        }

        public IMember GetMemberByID(int id)
        {
            if (!memberDict.ContainsKey(id))
            {
                return null;
            }
            return memberDict[id];
        }
    }
}