using BusinessAnalytic.Models.Abstract;
using BusinessAnalytic.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreEmployeeRepository : EfCoreGenericRepository<Employee, AdventureWorks2019Context>, IEmployeeRepository
    {
        private readonly AdventureWorks2019Context _context;
        public EfCoreEmployeeRepository(AdventureWorks2019Context context) : base(context)
        {
            _context = context;
        }

        public async Task<Employee> Details(int? id)
        {
            return await _context.Employees
                .Include(e => e.BusinessEntity)
                .FirstOrDefaultAsync(m => m.BusinessEntityId == id);
        }

        public bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.BusinessEntityId == id);
        }
    }
}
