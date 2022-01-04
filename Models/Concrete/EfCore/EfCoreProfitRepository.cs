using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreProfitRepository : EfCoreGenericRepository<ProductVendor, AdventureWorks2019Context>, IProfitRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreProfitRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductVendor> Details(int? id)
        {
            return await _context.ProductVendors
                .Include(p => p.BusinessEntity)
                .Include(p => p.Product)
                .Include(p => p.UnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
        }
        public async Task<ProductVendor> GetByTwoId(int? id, int? id2)
        {
            return await _context.ProductVendors.FindAsync(id, id2);
        }
        public bool ProductVendorExists(int id)
        {
            return _context.ProductVendors.Any(e => e.ProductId == id);
        }
    }
}
