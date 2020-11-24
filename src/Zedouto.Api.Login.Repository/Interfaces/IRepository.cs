using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Google.Cloud.Firestore;

namespace Zedouto.Api.Login.Repository.Interfaces
{
    public interface IRepository
    {
        Task<T> GetAsync<T>(Dictionary<string, object> filters, params string[] fieldsReturn);
        Task<IEnumerable<T>> ListAsync<T>(params string[] fieldsReturn);
        Task<string> AddAsync<T>(T entity);
    }
}