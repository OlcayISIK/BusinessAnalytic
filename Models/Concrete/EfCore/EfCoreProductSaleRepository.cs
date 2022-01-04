using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreProductSaleRepository : EfCoreGenericRepository<SalesOrderHeader, AdventureWorks2019Context>, IProductSalesRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreProductSaleRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }

        public async Task<SalesOrderHeader> Details(int? id)
        {
            return await _context.SalesOrderHeaders
                .Include(s => s.BillToAddress)
                .Include(s => s.CreditCard)
                .Include(s => s.CurrencyRate)
                .Include(s => s.Customer)
                .Include(s => s.SalesPerson)
                .Include(s => s.ShipMethod)
                .Include(s => s.ShipToAddress)
                .Include(s => s.Territory)
                .FirstOrDefaultAsync(m => m.SalesOrderId == id);
        }

        public bool SalesOrderHeaderExists(int id)
        {
            return _context.SalesOrderHeaders.Any(e => e.SalesOrderId == id);
        }
    }
}
