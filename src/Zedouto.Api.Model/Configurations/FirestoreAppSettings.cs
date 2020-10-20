using Newtonsoft.Json;

namespace Zedouto.Api.Model.Configurations
{
    public class FirestoreAppSettings
    {
        [JsonProperty]
        public string ProjectId { get; set; }

        [JsonProperty]
        public string CollectionName { get; set; }

        [JsonProperty]
        public string Credentials { get; set; }
    }
}