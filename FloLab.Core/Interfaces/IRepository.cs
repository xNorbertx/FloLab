using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace FloLab.Core.Interfaces
{
    public interface IRepository<T> where T: class
    {
        bool Exists(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Filter(Expression<Func<T, Boolean>> predicate);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Persist();
    }
}
