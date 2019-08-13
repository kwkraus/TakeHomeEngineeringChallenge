using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Iwannago.Data.EntityFrameworkCore.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected DbSet<T> DbSet;

        public EFRepository (DbContext context)
        {
            _context = context;
            DbSet = context.Set<T>();
        }

        public void Delete(T entity)
        {
            _context.Remove<T>(entity);
            _context.SaveChanges();
        }

        public T Get(Guid id)
        {
            return DbSet.Find(id);
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Update<T>(entity);
            _context.SaveChanges();
        }
    }
}
