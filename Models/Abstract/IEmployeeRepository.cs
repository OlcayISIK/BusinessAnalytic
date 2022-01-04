using BusinessAnalytic.Models.Entities;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> Details(int? id);
        bool EmployeeExists(int id);
    }
}
