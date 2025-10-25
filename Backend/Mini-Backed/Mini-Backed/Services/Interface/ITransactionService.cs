using Mini_Backed.Models;

namespace Mini_Backed.Services.Interface
{
    public interface ITransactionService
    {
        Task<IEnumerable<BorrowTransaction>> GetAllTransactionsAsync();
        Task<IEnumerable<BorrowTransaction>> GetOverdueTransactionsAsync();
    }
}
