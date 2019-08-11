using Iwannago.Data.Core.Interfaces;
using Iwannago.Data.Core.Models;
using System;

namespace Iwannago.Data.SqlServer.Repositories
{
    public class Repository<T> where T : BaseEntity, IRepository<T>
    {
        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
