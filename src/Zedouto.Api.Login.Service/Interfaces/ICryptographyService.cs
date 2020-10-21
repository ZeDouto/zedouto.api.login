using System.Threading.Tasks;

namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface ICryptographyService
    {
        Task<string> CryptographAsync(string text);
        Task<bool> EqualsTextAsync(string text, string cryptoText);
    }
}