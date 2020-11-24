using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zedouto.Api.Login.Repository.Interfaces
{
    public interface IFirestoreRepository : IRepository
    {
        Task<string> AddNestedAsync<T>(T entity, string nestedPropName, string firestoreDocumentId);
        Task<IEnumerable<T>> ContainsAsync<T>(string databaseField, IEnumerable<object> elements, params string[] fieldsToReturn);
    }
}