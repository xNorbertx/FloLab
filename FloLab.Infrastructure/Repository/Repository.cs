using FloLab.Core.Interfaces;
using FloLab.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FloLab.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T: class
    {
        private FloLabContext _context;
        private DbSet<T> _table;

        public Repository(FloLabContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public bool Exists(int id)
        {
            return _table.Find(id) != null;
        }

        public T Get(int id)
        {
            return _table.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList<T>();
        }

        public IEnumerable<T> Filter(Expression<Func<T, Boolean>> predicate)
        {
            return _table.Where(predicate);
        }

        public void Insert(T entity)
        {
            _context.Add(entity);
        }

        public void Update(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public void Persist()
        {
            _context.SaveChanges();
        }
    }
}
