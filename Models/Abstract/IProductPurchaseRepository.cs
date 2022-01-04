using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface IProductPurchaseRepository : IRepository<PurchaseOrderHeader>
    {
        Task<PurchaseOrderHeader> Details(int? id);
        bool PurchaseOrderHeaderExists(int id);
    }
}
