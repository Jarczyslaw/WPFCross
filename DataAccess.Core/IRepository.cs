using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public interface IRepository<T>
        where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Get(int id);
        void GetAll();
        void Get(Func<T, bool> predicate);
    }
}
