using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCorePeopleRepository : EfCoreGenericRepository<Person, AdventureWorks2019Context>, IPeopleRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCorePeopleRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
    }
}
