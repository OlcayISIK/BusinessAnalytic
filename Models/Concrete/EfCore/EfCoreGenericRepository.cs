using BusinessAnalytic.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity, TContext> : IRepository<TEntity>
           where TEntity : class
           where TContext : DbContext, new()
    {
        private TContext _context { get; set; }
        public EfCoreGenericRepository(TContext context)
        {
            _context = context;
        }
        public void Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
        }

        public IQueryable<TEntity> GetAll()
        {
                return _context.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int? id)
        {
                return await _context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
