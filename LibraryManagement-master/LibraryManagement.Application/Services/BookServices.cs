using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Models;
using LibraryManagement.Servisec.Base;

namespace LibraryManagement.Application.Services
{
    public class BookServices : IBookServices
    {


        public void Add(BookDTO book)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookInfo> GetAllBooks()
        {
            return new List<BookInfo>
            {
                new BookInfo { Id = 1, Title = "Science" },

                new BookInfo { Id = 2, Title = "Programming" }
            };
        }

        public BookInfo? GetBookById(int id)
        {
            return GetAllBooks().FirstOrDefault(b => b.Id == id);
        }

        public void Update(BookDTO book)
        {
            throw new NotImplementedException();
        }
    }

}