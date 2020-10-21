using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zedouto.Api.Service.Interfaces;

namespace Zedouto.Api.Service
{
    public class CryptographSHA512Service : ICryptographyService
    {
        private readonly byte[] _cryptoKey;

        public CryptographSHA512Service()
        {
            _cryptoKey = new byte[64];
        }
        
        public async Task<string> CryptographAsync(string text)
        {
            using (var hmac = new HMACSHA512(_cryptoKey))
            {
                var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
                var item = Encoding.UTF8.GetString(hashValue);

                return item;
            }
        }

        public async Task<bool> EqualsTextAsync(string text, string cryptoText)
        {
            var hash = await CryptographAsync(text);

            return hash.Equals(cryptoText);
        }
    }
}