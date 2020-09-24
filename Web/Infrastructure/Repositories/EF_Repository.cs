
using AppCore.Implementations;
using AppCore.Models;
using Infrastructure.DataTypes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EF_Repository<T> : IEF_Repository<T> where T : BaseEntity
    {
        EF_Context _context;

        public EF_Repository(EF_Context context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            var add = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return add.Entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            var delete = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return delete.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var updated = _context.Set<T>().Update(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var get = await _context.Set<T>().ToArrayAsync();
            return get;
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, int limit = 50, int page = 1)
        {
            var skipped = (page == 1? 0: page * limit);
            var get = await _context.Set<T>().Where(filter).Skip(skipped).Take(limit).ToArrayAsync();
            return get;
        }
        
    }
}
