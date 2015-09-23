using Library.Interfaces.Entities;
using System;

namespace Library.Entities
{
	public class Loan : ILoan
	{
		private IMember borrower;

		private IBook book;

		private DateTime borrowDate;

		private DateTime dueDate;

		private LoanState state;

		private int _id;

		public IBook Book
		{
			get
			{
				return book;
			}
		}

		public IMember Borrower
		{
			get
			{
				return borrower;
			}
		}

		public int ID
		{
			get
			{
				return _id;
			}
		}

		public bool IsOverDue
		{
			get
			{
				return state == LoanState.OVERDUE;
			}
		}

		LoanState Library.Interfaces.Entities.ILoan.State
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public LoanState State
		{
			get
			{
				return state;
			}
		}

		public Loan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate)
		{
			if (!sane(book, borrower, borrowDate, dueDate))
			{
				throw new ArgumentException("Loan: constructor : bad parameters");
			}
			book = book;
			borrower = borrower;
			borrowDate = borrowDate;
			dueDate = dueDate;
			_id = 0;
			state = LoanState.PENDING;
		}

		public bool CheckOverDue(DateTime currentDate)
		{
			if (state != LoanState.CURRENT && state != LoanState.OVERDUE)
			{
				throw new ApplicationException(string.Format("Loan : checkOverDue : incorrect state transition  :{0} -> {1}\n", state, LoanState.OVERDUE));
			}
			if (DateTime.Compare(currentDate, dueDate) > 0)
			{
				state = LoanState.OVERDUE;
			}
			return IsOverDue;
		}

		public void Commit(int id)
		{
			if (state != LoanState.PENDING)
			{
				throw new ApplicationException(string.Format("Loan : commit : incorrect state transition  : {0} -> {1}\n", state, LoanState.CURRENT));
			}
			if (id <= 0)
			{
				throw new ApplicationException(string.Format("Loan : commit : loan ID must be positive integer\n", new object[0]));
			}
			_id = id;
			state = LoanState.CURRENT;
			book.Borrow(this);
			borrower.AddLoan(this);
		}

		public void Complete()
		{
			if (state != LoanState.CURRENT && state != LoanState.OVERDUE)
			{
				throw new ApplicationException(string.Format("Loan : complete : incorrect state transition  : {0} -> {1}\n", state, LoanState.COMPLETE));
			}
			state = LoanState.COMPLETE;
		}

		private bool sane(IBook book, IMember borrower, DateTime borrowDate, DateTime returnDate)
		{
			if (book == null || borrower == null)
			{
				return false;
			}
			return DateTime.Compare(borrowDate, returnDate) <= 0;
		}

		public override string ToString()
		{
			string newLine = Environment.NewLine;
			return string.Format("{1,-20}\t{2} {0}{3,-20}\t{4} {0}{5,-20}\t{6} {0}{7,-20}\t{8} {9} {0}{10,-20}\t{11:d} {0}{12,-20}\t{13:d}", new object[] { newLine, "Loan ID:     ", _id, "Author:      ", book.Author, "Title:       ", book.Title, "Borrower:    ", borrower.FirstName, borrower.LastName, "Borrow Date: ", borrowDate, "Due Date:    ", dueDate });
		}
	}
}