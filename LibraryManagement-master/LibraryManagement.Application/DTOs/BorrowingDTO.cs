namespace LibraryManagement.Application.DTOs

{

    public class BorrowingDTO

    {

        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        public int BookId { get; set; }

        public string BookTitle { get; set; } = string.Empty;

        public DateTime BorrowDate { get; set; }

        public DateTime ReturnDate { get; set; }

    }

}
