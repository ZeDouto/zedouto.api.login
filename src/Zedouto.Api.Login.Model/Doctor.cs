using System.Text.Json.Serialization;
using Google.Cloud.Firestore;

namespace Zedouto.Api.Login.Model
{
    public class Doctor
    {
        [JsonPropertyName("crm")]
        public string Crm { get; set; }

        [JsonPropertyName("especialidade")]
        public string Specialty { get; set; }
    }
}