using System.Text.Json.Serialization;
using Zedouto.Api.Login.Model.Extensions;

namespace Zedouto.Api.Login.Model
{
    [JsonConverter(typeof(UserPropertyConverter))]
    public class User
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("senha")]
        public string Password { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("cpf")]
        public string Cpf { get; set; }

        [JsonPropertyName("medico")]
        public Doctor Doctor { get; set; }
    }

    public class UserWithoutConverterAttribute : User
    {
    }
}