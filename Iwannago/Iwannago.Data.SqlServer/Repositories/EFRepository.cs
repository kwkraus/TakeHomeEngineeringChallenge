using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.Core.Models;
using Iwannago.Data.Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iwannago.Data.EntityFrameworkCore.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected DbSet<T> _entities;

        public EFRepository(DbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Remove<T>(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetAsync(Guid id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetListAsync(Specification<T> spec)
        {
            return await _entities.Where(spec.ToExpression()).ToListAsync();
        }

        public async Task InsertAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var entityToEdit = _entities.Find(entity.Id);

            if (entityToEdit == null)
                throw new ArgumentException($"Couldn't find matching {nameof(T)} with Id={entity.Id}");

            _context.Entry(entityToEdit).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
