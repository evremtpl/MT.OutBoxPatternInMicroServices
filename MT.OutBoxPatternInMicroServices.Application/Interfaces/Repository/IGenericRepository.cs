using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MT.OutBoxPatternInMicroServices.Application.Interfaces.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {

        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
