using Mini_Backed.Models;

namespace Mini_Backed.Services.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> AddBookAsync(Book book);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<string> BorrowBookAsync(int userId, int bookId);
        Task<string> ReturnBookAsync(int transactionId);
        Task<IEnumerable<BorrowTransaction>> GetUserBorrowedBooksAsync(int userId);
    }
}
