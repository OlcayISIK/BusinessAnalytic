using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product, AdventureWorks2019Context>, IProductRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreProductRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
        public async Task<Product> Details(int? id)
        {
            return await _context.Set<Product>().Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
