using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OwlreportAPI.Data;
using OwlreportAPI.Models;

namespace OwlreportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeReportController : ControllerBase
    {
        private readonly DataContext _context;

        public TimeReportController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<object>>> Get()
        {
            var timereports = await _context.TimeReports.ToListAsync();
            var result = timereports.Select(p => new  {
                 TimeReportId=p.TimeReportId,
                 ProjectId=p.ProjectId,
                 UserName=p.UserName,
                 Comment=p.Comment,
                 Date=p.Date,
                 HoursWorked=p.HoursWorked,
                 ProjectName= _context.Projects.Where(t => t.ProjectId == p.ProjectId).FirstOrDefault().ProjectName,
            }).ToList();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<TimeReport>>> AddReport(TimeReport report)
        {
            _context.TimeReports.Add(report);
            await _context.SaveChangesAsync();
            return Ok("time report added");
        }

        
    
}
}
