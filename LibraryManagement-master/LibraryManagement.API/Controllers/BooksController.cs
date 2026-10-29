using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.API.Controllers
{
    public class BookImageRequest
    {
        public IFormFile? Image { get; set; }
    }

    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BooksController(
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var books = _context.Books
                .AsNoTracking()
                .Select(book => new
                {
                    book.Id,
                    book.Title,
                    book.Author,
                    book.Price,
                    book.CategoryId,
                    book.ImagePath
                })
                .ToList();

            return Ok(books);
        }

        [HttpPost("{id}/image")]
        [RequestSizeLimit(6 * 1024 * 1024)]
        public async Task<IActionResult> UploadImage(
            int id,
            [FromForm] BookImageRequest request)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            var image = request.Image;

            if (image == null || image.Length == 0)
            {
                return BadRequest(new { message = "Choose an image" });
            }

            if (image.Length > 5 * 1024 * 1024)
            {
                return BadRequest(new { message = "Maximum size is 5 MB" });
            }

            // التحقق من بداية الملف: PNG أو JPEG
            byte[] header = new byte[8];

            using (var input = image.OpenReadStream())
            {
                await input.ReadExactlyAsync(
                    header.AsMemory(0, (int)Math.Min(8, image.Length)));
            }

            bool isPng =
                image.Length >= 8 &&
                header[0] == 137 && header[1] == 80 &&
                header[2] == 78 && header[3] == 71 &&
                header[4] == 13 && header[5] == 10 &&
                header[6] == 26 && header[7] == 10;

            bool isJpeg =
                image.Length >= 3 &&
                header[0] == 255 &&
                header[1] == 216 &&
                header[2] == 255;

            if (!isPng && !isJpeg)
            {
                return BadRequest(new
                {
                    message = "Choose a PNG or JPEG image"
                });
            }

            string folder = Path.Combine(
                _environment.ContentRootPath,
                "wwwroot", "uploads", "books");

            Directory.CreateDirectory(folder);

            string extension = isPng ? ".png" : ".jpg";
            string fileName = Guid.NewGuid() + extension;
            string filePath = Path.Combine(folder, fileName);

            using (var output = System.IO.File.Create(filePath))
            {
                await image.CopyToAsync(output);
            }

            book.ImagePath = "/uploads/books/" + fileName;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Image uploaded successfully",
                book.Id,
                book.ImagePath
            });
        }
    }
}