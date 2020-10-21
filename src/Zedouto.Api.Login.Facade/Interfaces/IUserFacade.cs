using System.Threading.Tasks;
using Zedouto.Api.Login.Model.Entities;
using Zedouto.Api.Login.Model.Models;

namespace Zedouto.Api.Login.Facade.Interfaces
{
    public interface IUserFacade
    {
        Task AddUserAsync(User user);
        Task<UserToken> LoginAsync(User user);
    }
}