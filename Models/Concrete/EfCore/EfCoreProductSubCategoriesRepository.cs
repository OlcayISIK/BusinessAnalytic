using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreProductSubCategoriesRepository : EfCoreGenericRepository<ProductSubcategory, AdventureWorks2019Context>, IProductSubCategoriesRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreProductSubCategoriesRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
    }
}
