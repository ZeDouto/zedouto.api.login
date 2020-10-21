using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Zedouto.Api.Login.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetByPathIdAsync(string pathId);
        Task<T> GetAsync(Dictionary<string, object> filters);
        Task<IEnumerable<T>> ListAsync(Dictionary<string, object> filters);
        Task<Timestamp> AddAsync(T entity);
    }
}