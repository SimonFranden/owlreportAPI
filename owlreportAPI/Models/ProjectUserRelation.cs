namespace OwlreportAPI.Models
{
    public class ProjectUserRelation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public bool Active { get; set; } = true;
    }
}
