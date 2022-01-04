using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface IProductSalesRepository : IRepository<SalesOrderHeader>
    {
        Task<SalesOrderHeader> Details(int? id);
        bool SalesOrderHeaderExists(int id);
    }
}
