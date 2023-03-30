using System.ComponentModel.DataAnnotations.Schema;

namespace OwlreportAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string SecretKey { get; set; }
    }

    public class PublicUserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
    }


}
