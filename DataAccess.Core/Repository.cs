using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Core
{
    public abstract class Repository<T>
        where T : class
    {
        public string CollectionName { get; protected set; }
        private IDatabaseSource databaseSource;
        
        public Repository(IDatabaseSource databaseSource)
        {
            this.databaseSource = databaseSource;
        }

        public void Get(Expression<Func<T, bool>> predicate)
        {
            using (var db = databaseSource.OpenDatabase())
            {
                var collection = db.GetCollection<T>(CollectionName);
                collection.Find(predicate);
            }
        }

        public void Add(T entity)
        {
            using (var db = databaseSource.OpenDatabase())
            {
                var collection = db.GetCollection<T>(CollectionName);
                collection.Insert(entity);
            }
        }

        public void Update(T entity)
        {
            using (var db = databaseSource.OpenDatabase())
            {
                var collection = db.GetCollection<T>(CollectionName);
                collection.Update(entity);
            }
        }

        public int Delete(Expression<Func<T, bool>> predicate)
        {
            using (var db = databaseSource.OpenDatabase())
            {
                var collection = db.GetCollection<T>(CollectionName);
                return collection.Delete(predicate);
            }
        }
    }
}
