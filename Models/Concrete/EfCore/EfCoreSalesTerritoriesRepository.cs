using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreSalesTerritoriesRepository : EfCoreGenericRepository<SalesTerritory, AdventureWorks2019Context>, ISalesTerritoriesRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreSalesTerritoriesRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }

        public async Task<SalesTerritory> Details(int? id)
        {
            return await _context.SalesTerritories
                .Include(s => s.CountryRegionCodeNavigation)
                .FirstOrDefaultAsync(m => m.TerritoryId == id);
        }

        public bool SalesTerritoryExists(int id)
        {
            return _context.SalesTerritories.Any(e => e.TerritoryId == id);
        }
    }
}
