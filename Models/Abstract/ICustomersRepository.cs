using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface ICustomersRepository : IRepository<Customer>
    {
        Task<Customer> Details(int? id);
        bool CustomerExists(int id);
    }
}
