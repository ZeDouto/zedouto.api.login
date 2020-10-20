using System.Threading.Tasks;
using Zedouto.Api.Model.Entities;

namespace Zedouto.Api.Service.Interfaces
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(User user);
    }
}