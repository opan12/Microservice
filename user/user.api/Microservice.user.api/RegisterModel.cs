namespace Microservice.user.api
{
    public class RegisterModel
    {

        public string Username { get; set; }

        public string TCKimlikNO { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
