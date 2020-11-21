using System.Threading.Tasks;

namespace Zedouto.Api.Login.Service.Interfaces
{
    public interface ICryptographyService
    {
        string Cryptograph(string text);
        bool EqualsText(string text, string cryptoText);
    }
}