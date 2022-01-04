using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreUnitMeasuresRepository : EfCoreGenericRepository<UnitMeasure, AdventureWorks2019Context>, IUnitMeasuresRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreUnitMeasuresRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
    }
}
