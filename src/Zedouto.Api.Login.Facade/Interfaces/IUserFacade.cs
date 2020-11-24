using System.Collections.Generic;
using System.Threading.Tasks;
using Zedouto.Api.Login.Model;

namespace Zedouto.Api.Login.Facade.Interfaces
{
    public interface IUserFacade
    {
        Task AddUserAsync(User user);
        Task<UserToken> LoginAsync(User user);
        Task<UserToken> GetByCpfAsync(string cpf);
        Task<User> DeserializeTokenAsync(string token);
        Task<IEnumerable<User>> GetAllByCrmsAsync(params string[] crms);
    }
}