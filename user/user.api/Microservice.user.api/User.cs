using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Microservice.user.api
{
    public class User
    {
        [BsonId]  // MongoDB için benzersiz ID
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string MusteriNo { get; set; }
        public string Username { get; set; }
        public string TCKimlikNO { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime DogumTarihi { get; set; }
    }
}
