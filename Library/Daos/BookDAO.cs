using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace Library.Daos
{
    public class BookDAO : IBookDAO
    {
        private IBookHelper helper;

        private Dictionary<int, IBook> bookDict;

        private int nextID;

        public List<IBook> BookList
        {
            get
            {
                List<IBook> books = new List<IBook>();
                foreach (IBook value in bookDict.Values)
                {
                    books.Add(value);
                }
                return books;
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

        public BookDAO(IBookHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentException(string.Format("BookDAO : constructor : helper cannot be null.", new object[0]));
            }
            helper = helper;
            bookDict = new Dictionary<int, IBook>();
            nextID = 1;
        }

        public IBook AddBook(string author, string title, string callNo)
        {
            int nextID = NextID;
            IBook book = helper.MakeBook(author, title, callNo, nextID);
            bookDict.Add(nextID, book);
            return book;
        }

        public List<IBook> FindBooksByAuthor(string author)
        {
            return null;
        }

        public List<IBook> FindBooksByAuthorTitle(string author, string title)
        {
            return null;
        }

        public List<IBook> FindBooksByTitle(string title)
        {
            return null;
        }

        public IBook GetBookByID(int id)
        {
            if (!bookDict.ContainsKey(id))
            {
                return null;
            }
            return bookDict[id];
        }
    }
}