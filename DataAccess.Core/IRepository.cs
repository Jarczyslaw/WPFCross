using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess
{
    public interface IRepository<T>
        where T : class
    {
        void Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        int Delete(Expression<Func<T, bool>> predicate);
    }
}
