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

        public async Task<T> AddAsync(T entity)
        {
            return await _repository.AddAsync(entity);
        }

        public async Task<T> DeleteAsync(T entity)
        {
            return await _repository.DeleteAsync(entity);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await _repository.UpdateAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter, int limit = 50, int page = 1)
        {
            return await _repository.GetAsync(filter, limit, page);
        }
    }
}
