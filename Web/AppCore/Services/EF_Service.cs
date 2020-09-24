using AppCore.Implementations;
using AppCore.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class EF_Service<T> : IEF_Service<T> where T : BaseEntity
    {
        IEF_Repository<T> _repository;
        public EF_Service(IEF_Repository<T> repository)
        {
            _repository = repository;
        }

        public Task<T> AddAsync(T entity)
        {
            return _repository.AddAsync(entity);
        }

        public Task<T> DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return _repository.GetAll();
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, int limit = 50, int page = 1)
        {
            return _repository.GetAsync(filter, limit, page);
        }
    }
}
