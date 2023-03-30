namespace OwlreportAPI.Models
{
    public class UserToAdd
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }        
    }

    public class AddUsers
    {
        public string UserSecretKey { get; set; }
        public List<UserToAdd> UserList { get; set; }
    }

}
