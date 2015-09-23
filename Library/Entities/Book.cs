using Library.Interfaces.Entities;
using System;

namespace Library.Entities
{
	public class Book : IBook
	{
		private ILoan loan;

		private BookState state;

		private string author;

		private string title;

		private string callNumber;

		private int id;

		public string Author
		{
			get
			{
				return author;
			}
		}

		public string CallNumber
		{
			get
			{
				return callNumber;
			}
		}

		public int ID
		{
			get
			{
				return id;
			}
		}

		public ILoan Loan
		{
			get
			{
				return loan;
			}
		}

		public BookState State
		{
			get
			{
				return state;
			}
		}

		public string Title
		{
			get
			{
				return title;
			}
		}

		public Book(string author, string title, string callNumber, int bookID)
		{
			if (!sane(author, title, callNumber, bookID))
			{
				throw new ArgumentException("Member: constructor : bad parameters");
			}
			author = author;
			title = title;
			callNumber = callNumber;
			id = bookID;
			state = BookState.AVAILABLE;
			loan = null;
		}

		public void Borrow(ILoan loan)
		{
			if (loan == null)
			{
				throw new ArgumentException("Book: borrow : Bad parameter: loan cannot be null");
			}
			if (state != BookState.AVAILABLE)
			{
				throw new ApplicationException(string.Format("Illegal operation in state : {0}", state));
			}
			loan = loan;
			state = BookState.ON_LOAN;
		}

		public void Dispose()
		{
			if (state != BookState.AVAILABLE && state != BookState.DAMAGED && state != BookState.LOST)
			{
				throw new ApplicationException(string.Format("Illegal operation in state : {0}", state));
			}
			state = BookState.DISPOSED;
		}

		public void Lose()
		{
			if (state != BookState.ON_LOAN)
			{
				throw new ApplicationException(string.Format("Illegal operation in state : {0}", state));
			}
			state = BookState.LOST;
		}

		public void Repair()
		{
			if (state != BookState.DAMAGED)
			{
				throw new ApplicationException(string.Format("Illegal operation in state : {0}", state));
			}
			state = BookState.AVAILABLE;
		}

		public void ReturnBook(bool damaged)
		{
			if (state != BookState.ON_LOAN && state != BookState.LOST)
			{
				throw new ApplicationException(string.Format("Illegal operation in state : {0}", state));
			}
			loan = null;
			if (damaged)
			{
				state = BookState.DAMAGED;
				return;
			}
			state = BookState.AVAILABLE;
		}

		private bool sane(string author, string title, string callNumber, int bookID)
		{
			if (string.IsNullOrEmpty(author) || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(callNumber))
			{
				return false;
			}
			return bookID > 0;
		}

		public override string ToString()
		{
			string newLine = Environment.NewLine;
			return string.Format("{1,-20}\t{2} {0}{3,-20}\t{4} {0}{5,-20}\t{6} {0}{7,-20}\t{8}", new object[] { newLine, "Id:          ", id, "Call Number: ", callNumber, "Author:      ", author, "Title:       ", title });
		}
	}
}