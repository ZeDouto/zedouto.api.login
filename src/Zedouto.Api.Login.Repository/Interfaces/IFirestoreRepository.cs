using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zedouto.Api.Login.Repository.Interfaces
{
    public interface IFirestoreRepository : IRepository
    {
        Task<T> GetByDocumentId<T>(string documentId);
        Task<string> AddNestedAsync<T>(T entity, string nestedPropName, string firestoreDocumentId);
        Task<IEnumerable<T>> ContainsAsync<T>(Dictionary<string, IEnumerable<object>> elements, params string[] fieldsReturn);
        Task<IEnumerable<T>> ListAsync<T>(IEnumerable<Dictionary<string, object>> filters, params string[] fieldsReturn);
    }
}