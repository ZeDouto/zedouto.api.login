using System.Threading.Tasks;
using Zedouto.Api.Login.Model.Models;

namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface IJwtService<T> where T : class
    {
        Task<UserToken> GetToken(T model);
    }
}