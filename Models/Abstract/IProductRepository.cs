using BusinessAnalytic.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    //example Entity Repo
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Details(int? id);
        bool ProductExists(int id);
    }
}
