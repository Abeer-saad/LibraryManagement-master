using LibraryManagement.Models;

namespace LibraryManagement.Domain.Models
{
    public class Borrowing
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }
    }
}