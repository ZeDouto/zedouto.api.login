using System.Text.Json.Serialization;
using static Zedouto.Api.Login.Model.Constants.JsonPropertiesName;
using Zedouto.Api.Login.Model.Extensions;

namespace Zedouto.Api.Login.Model
{
    [JsonConverter(typeof(UserPropertyConverter))]
    public class User
    {
        [JsonPropertyName(LOGIN_PROPERTY_TEXT)]
        public string Login { get; set; }

        [JsonPropertyName(PASSWORD_PROPERTY_TEXT)]
        public string Password { get; set; }

        [JsonPropertyName(NAME_PROPERTY_TEXT)]
        public string Name { get; set; }

        [JsonPropertyName(CPF_PROPERTY_TEXT)]
        public string Cpf { get; set; }

        [JsonPropertyName(DOCTOR_PROPERTY_TEXT)]
        public Doctor Doctor { get; set; }
    }

    public class UserWithoutConverterAttribute : User
    {
    }
}