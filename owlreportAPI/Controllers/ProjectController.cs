using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OwlreportAPI.Data;
using OwlreportAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OwlreportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly DataContext _context;

        public ProjectController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Project>>> Get()
        {
            return Ok(await _context.Projects.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Project>>> Get(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if(project == null)
            {
                return BadRequest("Project not found");
            }
            else
            {
                return Ok(project);
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<TimeReport>>> AddReport(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return Ok("project added");
        }
    }
}
