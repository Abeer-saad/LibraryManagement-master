using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Models;

namespace LibraryManagement.Servisec.Base
{

    public interface IBookServices
       
    {

        List<BookInfo> GetAllBooks();

        BookInfo? GetBookById(int id);

        void Add(BookDTO book);

        void Update(BookDTO book);

        void Delete(int id);

    }

}