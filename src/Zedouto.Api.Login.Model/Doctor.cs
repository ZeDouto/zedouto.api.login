using System.Text.Json.Serialization;
using static Zedouto.Api.Login.Model.Constants.JsonPropertiesName;

namespace Zedouto.Api.Login.Model
{
    public class Doctor
    {
        [JsonPropertyName(CRM_PROPERTY_TEXT)]
        public string Crm { get; set; }

        [JsonPropertyName(SPECIALTY_PROPERTY_TEXT)]
        public string Specialty { get; set; }
    }
}