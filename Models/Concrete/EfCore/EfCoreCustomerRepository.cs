using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreCustomerRepository :  EfCoreGenericRepository<Customer, AdventureWorks2019Context>, ICustomersRepository
    {
        private readonly AdventureWorks2019Context _context;
    public EfCoreCustomerRepository(AdventureWorks2019Context context) : base(context)
    {
        _context = context;
    }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }

        public async Task<Customer> Details(int? id)
        {
            return await _context.Customers
                .Include(c => c.Person)
                .Include(c => c.Store)
                .Include(c => c.Territory)
                .FirstOrDefaultAsync(m => m.CustomerId == id);

        }
    }
}