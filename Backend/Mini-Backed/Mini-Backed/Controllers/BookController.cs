using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mini_Backed.Models;
using Mini_Backed.Services.Interface;

namespace Mini_Backed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var added = await _bookService.AddBookAsync(book);
            return Ok(added);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }

        [Authorize(Roles = "User")]
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableBooks()
        {
            var books = await _bookService.GetAvailableBooksAsync();
            return Ok(books);
        }

        // Borrow a book
        [Authorize(Roles = "User")]
        [HttpPost("borrow")]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowRequest req)
        {
            var result = await _bookService.BorrowBookAsync(req.UserId, req.BookId);
            return Ok(new { message = result });
        }

        // Return a book
        [Authorize(Roles = "User")]
        [HttpPost("return")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnRequest req)
        {
            var result = await _bookService.ReturnBookAsync(req.TransactionId);
            return Ok(new { message = result });
        }

        // Get user's borrowed books
        [Authorize(Roles = "User")]
        [HttpGet("borrowed/{userId}")]
        public async Task<IActionResult> GetBorrowedBooks(int userId)
        {
            var result = await _bookService.GetUserBorrowedBooksAsync(userId);
            return Ok(result);
        }
    }

    public class BorrowRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }

    public class ReturnRequest
    {
        public int TransactionId { get; set; }
    }
}
