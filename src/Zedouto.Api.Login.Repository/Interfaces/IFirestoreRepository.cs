using System.Threading.Tasks;

namespace Zedouto.Api.Login.Repository.Interfaces
{
    public interface IFirestoreRepository : IRepository
    {
        Task<string> AddNestedAsync<T>(T entity, string nestedPropName, string firestoreDocumentId);
    }
}