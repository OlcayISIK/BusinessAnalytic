using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Concrete.EfCore;
using BusinessAnalytic.Models.Entities;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreLocationRepository : EfCoreGenericRepository<Location, AdventureWorks2019Context>, ILocationsRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreLocationRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
    }
}
