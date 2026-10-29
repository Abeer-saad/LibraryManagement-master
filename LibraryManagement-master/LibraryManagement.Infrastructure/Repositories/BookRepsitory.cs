using LibraryManagement.Application.DTOs;
using LibraryManagement.Infrastructure.Repositories;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repositories

{
    public class BookRepository : IBookRepository

    {

        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)

        {

            _context = context;

        }

        public List<BookDTO> GetAll()

        {

            return _context.Books

                .Include(b => b.Category)

                .Select(b => new BookDTO

                {

                    Id = b.Id,

                    Title = b.Title,

                    Author = b.Author,

                    Price = b.Price,

                    CategoryId = b.CategoryId,

                    CategoryName = b.Category != null

                        ? b.Category.Name : null

                }).ToList();

        }

        public BookDTO? GetById(int id)

        {

            return GetAll().FirstOrDefault(b => b.Id == id);

        }

        public void Add(BookDTO dto)

        {

            var book = new Book

            {

                Title = dto.Title,

                Author = dto.Author,

                Price = dto.Price,

                CategoryId = dto.CategoryId

            };

            _context.Books.Add(book);

            _context.SaveChanges();

        }

        public void Update(BookDTO dto)

        {

            var book = _context.Books.Find(dto.Id);

            if (book == null) return;

            book.Title = dto.Title;

            book.Author = dto.Author;

            book.Price = dto.Price;

            book.CategoryId = dto.CategoryId;

            _context.SaveChanges();

        }

        public void Delete(int id)

        {

            var book = _context.Books.Find(id);

            if (book == null) return;

            _context.Books.Remove(book);

            _context.SaveChanges();

        }
    }
}