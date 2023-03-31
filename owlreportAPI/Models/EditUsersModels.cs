namespace OwlreportAPI.Models
{
    public class UserToEdit
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }        
    }

    public class EditUsers
    {
        public string UserSecretKey { get; set; }
        public List<UserToEdit> UserList { get; set; }
    }


}
