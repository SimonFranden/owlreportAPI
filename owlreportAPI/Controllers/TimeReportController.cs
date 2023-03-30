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

        
    
}
}
