using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace Library.Entities
{
	public class Member : IMember
	{
		private float fineAmount;

		private List<ILoan> loanList;

		private string firstName;

		private string lastName;

		private string contactPhone;

		private string emailAddress;

		private int id;

		private MemberState _state;

		private bool BorrowingAllowed
		{
			get
			{
				if (HasReachedFineLimit || HasReachedLoanLimit)
				{
					return false;
				}
				return !HasOverDueLoans;
			}
		}

		public string ContactPhone
		{
			get
			{
				return contactPhone;
			}
		}

		public string EmailAddress
		{
			get
			{
				return emailAddress;
			}
		}

		public float FineAmount
		{
			get
			{
				return fineAmount;
			}
		}

		public string FirstName
		{
			get
			{
				return firstName;
			}
		}

		public bool HasFinesPayable
		{
			get
			{
				return fineAmount > 0f;
			}
		}

		public bool HasOverDueLoans
		{
			get
			{
				bool flag;
				List<ILoan>.Enumerator enumerator = loanList.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						if (!enumerator.Current.IsOverDue)
						{
							continue;
						}
						flag = true;
						return flag;
					}
					return false;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
		}

		public bool HasReachedFineLimit
		{
			get
			{
				return fineAmount >= 10f;
			}
		}

		public bool HasReachedLoanLimit
		{
			get
			{
				return loanList.Count >= 5;
			}
		}

		public int ID
		{
			get
			{
				return id;
			}
		}

		public string LastName
		{
			get
			{
				return lastName;
			}
		}

		MemberState Library.Interfaces.Entities.IMember.State
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public List<ILoan> Loans
		{
			get
			{
				return new List<ILoan>(loanList);
			}
		}

		public MemberState State
		{
			get
			{
				return _state;
			}
		}

		public Member(string firstName, string lastName, string contactPhone, string email, int memberID)
		{
			if (!sane(firstName, lastName, contactPhone, email, memberID))
			{
				throw new ArgumentException("Member: constructor : bad parameters");
			}
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactPhone = contactPhone;
			emailAddress = email;
			id = memberID;
			loanList = new List<ILoan>();
			fineAmount = 0f;
			_state = MemberState.BORROWING_ALLOWED;
		}

		public void AddFine(float fine)
		{
			if (fine < 0f)
			{
				throw new ArgumentException("Member: AddFine : fine cannot be negative");
			}
			fineAmount = fineAmount + fine;
			updateState();
		}

		public void AddLoan(ILoan loan)
		{
			if (loan == null)
			{
				throw new ArgumentException("Member: AddLoan : loan cannot be null");
			}
			if (!BorrowingAllowed)
			{
				throw new ApplicationException(string.Format("Member: AddLoan : illegal operation in state: {0}", _state));
			}
			loanList.Add(loan);
			updateState();
		}

		public void PayFine(float payment)
		{
			if (payment < 0f || payment > fineAmount)
			{
				throw new ArgumentException("Member: PayFine : payment cannot be negative, or greater than amount owed");
			}
			fineAmount = fineAmount - payment;
			updateState();
		}

		public void RemoveLoan(ILoan loan)
		{
			if (loan == null)
			{
				throw new ArgumentException("Member: RemoveLoan : loan cannot be null");
			}
			if (!loanList.Contains(loan))
			{
				throw new ArgumentException("Member: RemoveLoan : loan not present");
			}
			loanList.Remove(loan);
			updateState();
		}

		private bool sane(string firstName, string lastName, string contactPhone, string emailAddress, int memberID)
		{
			if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(contactPhone) || string.IsNullOrEmpty(emailAddress))
			{
				return false;
			}
			return memberID > 0;
		}

		public override string ToString()
		{
			string newLine = Environment.NewLine;
			return string.Format("{1,-20}\t{2} {0}{3,-20}\t{4}{5} {0}{6,-20}\t{7} {0}{8,-20}\t{9} {0}{10,-20}\t{11:$0.00}", new object[] { newLine, "Id:                  ", id, "Name:                ", firstName, lastName, "Contact Phone:       ", contactPhone, "Email Address:       ", emailAddress, "Outstanding Charges: ", fineAmount });
		}

		private void updateState()
		{
			if (BorrowingAllowed)
			{
				_state = MemberState.BORROWING_ALLOWED;
				return;
			}
			_state = MemberState.BORROWING_DISALLOWED;
		}
	}
}