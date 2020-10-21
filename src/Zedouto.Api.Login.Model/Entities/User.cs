using Google.Cloud.Firestore;

namespace Zedouto.Api.Login.Model.Entities
{
    [FirestoreData]
    public class User
    {
        [FirestoreProperty]
        public string Login { get; set; }

        [FirestoreProperty]
        public string Password { get; set; }
    }
}