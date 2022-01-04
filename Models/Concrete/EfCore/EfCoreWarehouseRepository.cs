using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreWarehouseRepository : EfCoreGenericRepository<ProductInventory, AdventureWorks2019Context>, IWarehousesRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreWarehouseRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }
        public bool ProductInventoryExists(int id)
        {
            return _context.ProductInventories.Any(e => e.ProductId == id);
        }
    }
}
