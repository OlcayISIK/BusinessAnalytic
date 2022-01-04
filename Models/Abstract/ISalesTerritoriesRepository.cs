using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface ISalesTerritoriesRepository : IRepository<SalesTerritory>
    {
        Task<SalesTerritory> Details(int? id);
        bool SalesTerritoryExists(int id);
    }
}

