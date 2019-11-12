using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Iwannago.Data.Core.Models;
using Iwannago.Data.Core.Specifications;

namespace Iwannago.Data.EntityFrameworkCore.Repositories
{
    public interface IEFRepository<T> where T : BaseEntity
    {
        Task DeleteAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task<IReadOnlyList<T>> GetListAsync(Specification<T> spec);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
    }
}