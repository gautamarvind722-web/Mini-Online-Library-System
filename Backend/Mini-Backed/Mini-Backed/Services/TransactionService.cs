using Microsoft.EntityFrameworkCore;
using Mini_Backed.Models;
using Mini_Backed.Services.Interface;

namespace Mini_Backed.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly MiniOnlineLibraryContext _context;

        public TransactionService(MiniOnlineLibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BorrowTransaction>> GetAllTransactionsAsync()
        {
            return await _context.BorrowTransactions
                .Include(t => t.Book)
                .Include(t => t.User)
                .OrderByDescending(t => t.BorrowDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowTransaction>> GetOverdueTransactionsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.BorrowTransactions
                .Include(t => t.Book)
                .Include(t => t.User)
                .Where(t => t.Status == "Borrowed" && t.BorrowDate.AddDays(2) < now)
                .ToListAsync();
        }
    }
}
