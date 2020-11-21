using Google.Cloud.Firestore;

namespace Zedouto.Api.Login.Model.Entities
{
    [FirestoreData(UnknownPropertyHandling.Ignore)]
    public class DoctorEntity
    {
        [FirestoreProperty]
        public string Crm { get; set; }

        [FirestoreProperty]
        public string Specialty { get; set; }
    }
}