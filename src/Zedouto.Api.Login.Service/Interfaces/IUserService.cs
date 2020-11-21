using System.Threading.Tasks;
using Zedouto.Api.Login.Model;

namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(User user);
        Task<User> GetByLoginAndSenhaAsync(User user);
    }
}