using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IGenRepository<T> where T : Entity
    {
        Task<T> GetAsync<TOrderKey>(Expression<Func<T, bool>> where,
           Expression<Func<T, TOrderKey>> orderBy = null,
           SortOrder sort = SortOrder.Unspecified);


        Task<T> GetAsync(Expression<Func<T, bool>> where);           
        Task<List<T>> GetAllAsync(params Expression<Func<T, Object>>[] includes);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where, params Expression<Func<T, Object>>[] includes);



        IQueryable<T> Set();
        IQueryable<T> SetAsNoTracking();
        int CreateItem(T item);
        int CreateItems(IEnumerable<T> entities);
        int Delete(T item);
        int DeleteRange(IEnumerable<T> items);
        int Update(T item);        
    }
}
