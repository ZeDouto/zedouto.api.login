using System.Threading.Tasks;
using Zedouto.Api.Model.Models;

namespace Zedouto.Api.Service.Interfaces
{
    public interface IJwtService<T> where T : class
    {
        Task<UserToken> GetToken(T model);
    }
}