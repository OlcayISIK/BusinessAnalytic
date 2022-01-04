using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface IProfitRepository : IRepository<ProductVendor>
    {
        Task<ProductVendor> Details(int? id);
        bool ProductVendorExists(int id);
        Task<ProductVendor> GetByTwoId(int? id,int? id2);
    }
}
