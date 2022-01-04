using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface IProductTransactionRepository : IRepository<TransactionHistory>
    {
        Task<TransactionHistory> Details(int? id);
        bool TransactionHistoryExists(int id);
    }
}
