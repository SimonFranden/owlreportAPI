namespace OwlreportAPI.Models
{
    public class TimeReport
    {

        public int TimeReportId { get; set; }
        public int ProjectId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;       
        public int HoursWorked { get; set; } = 0;
    }
}
//Date
//coment