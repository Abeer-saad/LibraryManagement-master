using LibraryManagement.Application.DTOs;
using LibraryManagement.Domain.Models;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Models;

namespace LibraryManagement.Repositories
{

    public class BorrowingRepository : IBorrowingRepository

    {

        private readonly ApplicationDbContext _context;

        public BorrowingRepository(ApplicationDbContext context)

        {

            _context = context;

        }

        public List<BorrowingDTO> GetAll()

        {

            return _context.Borrowings

                .Select(b => new BorrowingDTO

                {

                    Id = b.Id,

                    CustomerId = b.CustomerId,

                    CustomerName = b.Customer.Name,

                    BookId = b.BookId,

                    BookTitle = b.Book.Title,

                    BorrowDate = b.BorrowDate,

                    ReturnDate = b.ReturnDate

                })

                .ToList();

        }

        public BorrowingDTO? GetById(int id)

        {

            return _context.Borrowings

                .Where(b => b.Id == id)

                .Select(b => new BorrowingDTO

                {

                    Id = b.Id,

                    CustomerId = b.CustomerId,

                    CustomerName = b.Customer.Name,

                    BookId = b.BookId,

                    BookTitle = b.Book.Title,

                    BorrowDate = b.BorrowDate,

                    ReturnDate = b.ReturnDate

                })

                .FirstOrDefault();

        }

        public void Add(BorrowingDTO borrowing)

        {

            var entity = new Borrowing

            {

                CustomerId = borrowing.CustomerId,

                BookId = borrowing.BookId,

                BorrowDate = borrowing.BorrowDate,

                ReturnDate = borrowing.ReturnDate

            };

            _context.Borrowings.Add(entity);

            _context.SaveChanges();

        }

        public void Update(BorrowingDTO borrowing)

        {

            var entity = _context.Borrowings.Find(borrowing.Id);

            if (entity == null) return;

            entity.CustomerId = borrowing.CustomerId;

            entity.BookId = borrowing.BookId;

            entity.BorrowDate = borrowing.BorrowDate;

            entity.ReturnDate = borrowing.ReturnDate;

            _context.SaveChanges();

        }

        public void Delete(int id)

        {

            var entity = _context.Borrowings.Find(id);

            if (entity == null) return;

            _context.Borrowings.Remove(entity);

            _context.SaveChanges();

        }

    }

}