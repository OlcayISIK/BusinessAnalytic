using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface IWarehousesRepository : IRepository<ProductInventory>
    {
        bool ProductInventoryExists(int id);
    }
}
