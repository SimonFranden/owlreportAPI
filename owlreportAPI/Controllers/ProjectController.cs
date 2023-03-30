using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

        
        List<PublicUserInfo> GetProjectMemebers(int projectId)
        {
            List<ProjectUserRelation> projectUserRelationList = _context
                .ProjectUserRelations
                .Where(pur => pur.ProjectId == projectId)
                .ToList();

            List<PublicUserInfo> users = new();
            foreach(ProjectUserRelation pur in projectUserRelationList)
            {
                User foundUser = _context
                    .Users
                    .Where(user => user.Id == pur.UserId)
                    .SingleOrDefault();

                PublicUserInfo projectMember = new();
                projectMember.Id = foundUser.Id;
                projectMember.Username = foundUser.Username;
                projectMember.FName = foundUser.FName;
                projectMember.LName = foundUser.LName;

                users.Add(projectMember);
            }
            return users;
        }

        [HttpGet("ProjectsInfo")]
        public async Task<ActionResult<IEnumerable<object>>> GetTotalHours()
        {
            var projects = await _context.Projects.ToListAsync();

            var result = projects.Select(p => new
            {
                ProjectId = p.ProjectId,
                ProjectOwner = p.ProjectOwner,
                ProjectName = p.ProjectName,
                ProjectLength = p.ProjectLength,
                TotalHours = _context.TimeReports.Where(t => t.ProjectId == p.ProjectId).Sum(t => t.HoursWorked),
                ProjectMembers = GetProjectMemebers(p.ProjectId)
                }).ToList();
                    

            return Ok(result);
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
       
    }
}
