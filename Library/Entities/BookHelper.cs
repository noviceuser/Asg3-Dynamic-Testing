using Library.Interfaces.Entities;
using System;

namespace Library.Entities
{
	public class BookHelper : IBookHelper
	{
		public BookHelper()
		{
		}

		public IBook MakeBook(string author, string title, string callNumber, int id)
		{
			return new Book(author, title, callNumber, id);
		}
	}
}