using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreVendorsRepository : EfCoreGenericRepository<Vendor, AdventureWorks2019Context>, IVendorsRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreVendorsRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
    }
}
