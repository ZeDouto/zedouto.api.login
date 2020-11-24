using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Zedouto.Api.Login.Repository.Interfaces
{
    public interface IRepository
    {
        Task<T> GetByPathIdAsync<T>(string pathId);
        Task<T> GetAsync<T>(Dictionary<string, object> filters);
        Task<IEnumerable<T>> ListAsync<T>(IEnumerable<Dictionary<string, object>> filters);
        Task<string> AddAsync<T>(T entity);
    }
}