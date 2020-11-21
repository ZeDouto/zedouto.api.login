using Google.Cloud.Firestore;

namespace Zedouto.Api.Login.Model.Entities
{
    [FirestoreData]
    public class UserEntity
    {
        [FirestoreProperty]
        public string Login { get; set; }

        [FirestoreProperty]
        public string Password { get; set; }

        [FirestoreProperty]
        public string Name { get; set; }

        [FirestoreProperty]
        public string Cpf { get; set; }

        [FirestoreProperty]
        public DoctorEntity Doctor { get; set; }
    }
}