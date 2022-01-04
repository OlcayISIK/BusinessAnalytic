using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreProductTransactionsRepository : EfCoreGenericRepository<TransactionHistory, AdventureWorks2019Context>, IProductTransactionRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreProductTransactionsRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }

        public async Task<TransactionHistory> Details(int? id)
        {
           return await _context.TransactionHistories
                 .Include(t => t.Product)
                 .FirstOrDefaultAsync(m => m.TransactionId == id);
        }

        public bool TransactionHistoryExists(int id)
        {
            return _context.TransactionHistories.Any(e => e.TransactionId == id);
        }
    }
}
