using Iwannago.Data.Core.Models;
using Iwannago.Data.Core.Specifications;
using System;
using System.Collections.Generic;

namespace Iwannago.Data.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Get(Guid id);
        IReadOnlyList<T> GetList(Specification<T> spec);
    }
}
