using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreProductModelRepository : EfCoreGenericRepository<ProductModel, AdventureWorks2019Context>, IProductModelsRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreProductModelRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
    }
}
