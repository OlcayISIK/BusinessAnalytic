using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreProductPurchaseRepository : EfCoreGenericRepository<PurchaseOrderHeader, AdventureWorks2019Context>, IProductPurchaseRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreProductPurchaseRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }

        public async Task<PurchaseOrderHeader> Details(int? id)
        {
            return await _context.PurchaseOrderHeaders
                .Include(p => p.Employee)
                .Include(p => p.ShipMethod)
                .Include(p => p.Vendor)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);
        }

        public bool PurchaseOrderHeaderExists(int id)
        {
            return _context.PurchaseOrderHeaders.Any(e => e.PurchaseOrderId == id);
        }
    }
}
