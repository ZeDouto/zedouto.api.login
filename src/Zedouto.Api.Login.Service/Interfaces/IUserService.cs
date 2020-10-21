using System.Threading.Tasks;
using Zedouto.Api.Login.Model.Entities;

namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(User user);
    }
}