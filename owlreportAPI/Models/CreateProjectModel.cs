namespace OwlreportAPI.Models
{
    public class CreateProjectModel
    {
        
        public string ProjectName { get; set; } = string.Empty;
        public int ProjectLength { get; set; }
        public string UserSecretKey { get; set; }
    }

    public class EditProjectModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; } = string.Empty;
        public int ProjectLength { get; set; }
        public string UserSecretKey { get; set; }
    }

    public class DeleteProjectModel
    {
        public int ProjectId { get; set; }
        public string UserSecretKey { get; set; }
    }
}
