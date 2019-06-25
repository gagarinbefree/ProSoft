using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess
{
    public interface IGenRepository<T>
    {
        T Get(Expression<Func<T, Object>>[] includes = null, Expression<Func<T, bool>> where = null);
        List<T> GetAll(Expression<Func<T, Object>>[] includes = null, Expression<Func<T, bool>> where = null, int limit = 0);
        IQueryable<T> Set();
        IQueryable<T> SetAsNoTracking();
        int CreateItem(T item);
        int CreateItems(IEnumerable<T> entities);
        int Delete(T item);
        int DeleteRange(IEnumerable<T> items);
        int Update(T item);
    }
}
