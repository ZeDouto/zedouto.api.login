using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Zedouto.Api.Login.Service.Interfaces;

namespace Zedouto.Api.Login.Service
{
    public class CryptographSHA512Service : ICryptographyService
    {
        private readonly byte[] _cryptoKey;

        public CryptographSHA512Service()
        {
            _cryptoKey = new byte[64];
        }
        
        public string Cryptograph(string text)
        {
            using (var hmac = new HMACSHA512(_cryptoKey))
            {
                var hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(text));
                return Encoding.UTF8.GetString(hashValue);
            }
        }

        public bool EqualsText(string text, string cryptoText)
        {
            var hash = Cryptograph(text);

            return hash.Equals(cryptoText);
        }
    }
}