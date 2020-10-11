using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Zedouto.Api.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> filter);
        Task<Timestamp> AddAsync(T entity);
        Task<T> EditAsync(T entity);
    }
}