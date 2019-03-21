using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClone.Data.Repositories
{
    public interface IGenericRepository<T, TId> where T : class, IEntity<TId> 
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes);

        Task<T> GetByIdAsync(TId id);

        Task<IEnumerable<T>> FindByConditionAync(Expression<Func<T, bool>> expression);

        /*
         * Add, Update, and Delete methods are not async as
         * they just track changes to an entity and wait for the EF Core’s SaveChanges method to execute.
         */

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        
    }
}
