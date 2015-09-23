using Library.Interfaces.Entities;
using System;

namespace Library.Entities
{
	public class LoanHelper : ILoanHelper
	{
		public LoanHelper()
		{
		}

		public ILoan MakeLoan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate)
		{
			return new Loan(book, borrower, borrowDate, dueDate);
		}
	}
}