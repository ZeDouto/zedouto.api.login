using System.Threading.Tasks;
using Zedouto.Api.Model.Entities;
using Zedouto.Api.Model.Models;

namespace Zedouto.Api.Facade.Interfaces
{
    public interface IUserFacade
    {
        Task AddUserAsync(User user);
        Task<UserToken> LoginAsync(User user);
    }
}