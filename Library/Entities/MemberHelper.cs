using Library.Interfaces.Entities;
using System;

namespace Library.Entities
{
	public class MemberHelper : IMemberHelper
	{
		public MemberHelper()
		{
		}

		public IMember MakeMember(string firstName, string lastName, string contactPhone, string emailAddress, int id)
		{
			return new Member(firstName, lastName, contactPhone, emailAddress, id);
		}
	}
}