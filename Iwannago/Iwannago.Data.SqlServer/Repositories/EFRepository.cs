using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.Core.Models;
using Iwannago.Data.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iwannago.Data.EntityFrameworkCore.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected DbSet<T> _dbSet;

        public EFRepository (DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(T entity)
        {
            _context.Remove<T>(entity);
            _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IReadOnlyList<T> GetList(Specification<T> spec)
        {
            return _dbSet.Where(spec.ToExpression()).ToList();
        }

        public void Insert(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update<T>(entity);
            _context.SaveChanges();
        }
    }
}
