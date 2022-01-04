using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Abstract
{
    public interface IRepository<T>
    {
        Task<T> GetById(int? id);
        IQueryable<T> GetAll();
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
