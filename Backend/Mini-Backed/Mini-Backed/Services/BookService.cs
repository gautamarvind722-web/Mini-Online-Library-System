using Microsoft.EntityFrameworkCore;
using Mini_Backed.Models;
using Mini_Backed.Services.Interface;

namespace Mini_Backed.Services
{
    public class BookService : IBookService
    {
        private readonly MiniOnlineLibraryContext _context;

        public BookService(MiniOnlineLibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            var borrowedBookIds = await _context.BorrowTransactions
                .Where(bt => bt.Status == "Borrowed")
                .Select(bt => bt.BookId)
                .ToListAsync();

            return await _context.Books
                .Where(b => !borrowedBookIds.Contains(b.BookId))
                .ToListAsync();
        }

        public async Task<string> BorrowBookAsync(int userId, int bookId)
        {
            // User cannot borrow more than 2 active books
            var activeCount = await _context.BorrowTransactions
                .CountAsync(bt => bt.UserId == userId && bt.Status == "Borrowed");
            if (activeCount >= 2)
                return "Cannot borrow more than 2 books at a time.";

            var book = await _context.Books.FindAsync(bookId);
            if (book == null) return "Book not found.";

            var transaction = new BorrowTransaction
            {
                UserId = userId,
                BookId = bookId,
                BorrowDate = DateTime.UtcNow,
                Status = "Borrowed"
            };

            _context.BorrowTransactions.Add(transaction);
            await _context.SaveChangesAsync();

            return "Book borrowed successfully.";
        }

        public async Task<string> ReturnBookAsync(int transactionId)
        {
            var transaction = await _context.BorrowTransactions.FindAsync(transactionId);
            if (transaction == null) return "Transaction not found.";
            if (transaction.Status == "Returned") return "Book already returned.";

            transaction.Status = "Returned";
            transaction.ReturnDate = DateTime.UtcNow;

            _context.BorrowTransactions.Update(transaction);
            await _context.SaveChangesAsync();

            return "Book returned successfully.";
        }

        public async Task<IEnumerable<BorrowTransaction>> GetUserBorrowedBooksAsync(int userId)
        {
            return await _context.BorrowTransactions
                .Include(bt => bt.Book)
                .Where(bt => bt.UserId == userId)
                .OrderByDescending(bt => bt.BorrowDate)
                .ToListAsync();
        }
    }
}
