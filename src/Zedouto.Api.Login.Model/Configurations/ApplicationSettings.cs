using Newtonsoft.Json;

namespace Zedouto.Api.Login.Model.Configurations
{
    public class ApplicationSettings
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string Version { get; set; }
    }
}