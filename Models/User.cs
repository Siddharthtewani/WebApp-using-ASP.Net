namespace FrndshipApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public byte[] PaswwordHash { get; set; }
        public byte[] PaswwordSalt { get; set; }
    }
}