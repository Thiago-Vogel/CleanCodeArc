using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Implementations
{
    public interface IEF_Service<T> where T:BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, int limit = 50, int page = 1);
        Task<IEnumerable<T>> GetAll();
    }
}
