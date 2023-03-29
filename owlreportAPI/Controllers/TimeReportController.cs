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
        public async Task<ActionResult<List<TimeReport>>> Get()
        {
            return Ok(await _context.TimeReports.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<TimeReport>>> AddReport(TimeReport report)
        {
            _context.TimeReports.Add(report);
            await _context.SaveChangesAsync();
            return Ok("time report added");
        }

        [HttpGet("total-hours")]
        public async Task<ActionResult<IEnumerable<object>>> GetTotalHours()
        {
            var projects = await _context.Projects.ToListAsync();

            var result = projects.Select(p => new
            {
                ProjectName = p.ProjectName,
                TotalHours = _context.TimeReports.Where(t => t.ProjectId == p.ProjectId).Sum(t => t.HoursWorked)
            }).ToList();

            return Ok(result);
        }
    
}
}
