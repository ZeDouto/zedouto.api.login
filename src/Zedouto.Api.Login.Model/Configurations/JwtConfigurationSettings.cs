using Newtonsoft.Json;

namespace Zedouto.Api.Login.Model.Configurations
{
    public class JwtConfigurationSettings
    {
        [JsonProperty]
        public string Key { get; set; }

        [JsonProperty]
        public int HourExpiration { get; set; }
    }
}