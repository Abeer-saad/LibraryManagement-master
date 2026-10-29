using LibraryManagement.Application;
using LibraryManagement.Servisec.Base;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers

{

    [ApiController]

    [Route("api/books")]

    public class BooksApiController : ControllerBase

    {

        private readonly IBookServices _bookService;

        public BooksApiController(IBookServices bookService)

        {

            _bookService = bookService;

        }

        // Endpoint 1 - Get All Books

        [HttpGet]

        public IActionResult GetAll()

        {

            var books = _bookService.GetAllBooks();

            return Ok(books);

        }

        // Endpoint 2 - Get Book By ID

        [HttpGet("{id:int}")]

        public IActionResult GetById(int id)

        {

            var book = _bookService.GetBookById(id);

            if (book == null)

            {

                return NotFound();

            }

            return Ok(book);

        }

    }

}