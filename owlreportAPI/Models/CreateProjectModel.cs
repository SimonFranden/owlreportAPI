namespace OwlreportAPI.Models
{
    public class CreateProjectModel
    {
        
        public string ProjectName { get; set; } = string.Empty;
        public int ProjectLength { get; set; }
        public string UserSecretKey { get; set; }
    }
}
