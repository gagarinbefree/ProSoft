﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Ef
{
    public class Repository<T> : IGenRepository<T> where T : class
    {
        private ProSoftDbContext _db;

        public Repository(ProSoftDbContext db)
        {
            _db = db;
        }

        public T Get(Expression<Func<T, Object>>[] includes = null, Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> q = _db.Set<T>();

            if (includes != null)
            {
                foreach (Expression<Func<T, Object>> include in includes)
                {
                    q = q.Include(include);
                }
            }

            return q.SingleOrDefault(where);
        }

        public List<T> GetAll(Expression<Func<T, Object>>[] includes = null, Expression<Func<T, bool>> where = null, int limit = 0)
        {
            IQueryable<T> q = _db.Set<T>();

            if (includes != null)
            {
                foreach (Expression<Func<T, Object>> include in includes)
                {
                    q = q.Include(include);
                }
            }

            if (where != null)
                q = q.Where(where);

            if (limit > 0)
                q = q.Take(limit);

            return q.ToList();
        }

        public IQueryable<T> Set()
        {
            return _db.Set<T>();
        }

        public IQueryable<T> SetAsNoTracking()
        {
            return _db.Set<T>().AsNoTracking<T>();
        }

        public int CreateItem(T item)
        {
            if (item == null)
                throw new Exception("Пустой объект для сохранения");

            _db.Set<T>().Add(item);

            int res = _db.SaveChanges();

            return res;
        }

        public int CreateItems(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new Exception("Список объектов для сохранения is null");

            if (entities.Count() == 0)
                return 0;

            _db.Set<T>().AddRange(entities);

            int res = _db.SaveChanges();

            return res;
        }
        
        public int DeleteRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                _db.Set<T>().Attach(item);
            }

            _db.Set<T>().RemoveRange(items);

            return _db.SaveChanges();
        }

        public int Delete(T item)
        {
            _db.Set<T>().Remove(item);

            return _db.SaveChanges();
        }

        public int Update(T item)
        {
            _db.Set<T>().Attach(item);
            _db.Entry(item).State = EntityState.Modified;

            return _db.SaveChanges();
        }
    }
}
