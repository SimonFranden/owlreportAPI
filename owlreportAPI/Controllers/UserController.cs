using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OwlreportAPI.Data;
using OwlreportAPI.Migrations;
using OwlreportAPI.Models;

namespace OwlreportAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("{userSecretKey}")]
        public async Task<ActionResult<List<User>>> Get(string userSecretKey)
        {            
            var foundUser = FindUserWithSecretKey(userSecretKey);
            
            if(foundUser == null)
            {
                return BadRequest("User not found");
            }

            return Ok(new
            {
                username = foundUser.Username,
                fName = foundUser.FName,
                lName = foundUser.LName,
            });
        }

        [HttpGet("UserProjects")]
        public async Task<ActionResult<List<User>>> GetUserProjects(string userSecretKey)
        {
            var foundUser = FindUserWithSecretKey(userSecretKey);

            if (foundUser == null)
            {
                return BadRequest("User not found");
            }

            var projectUserRelations = _context
                .ProjectUserRelations
                .Where(dbProjectUserRelation => dbProjectUserRelation.UserId == foundUser.Id).ToList();

            List<object> UserProjects = new();
            foreach(ProjectUserRelation item in projectUserRelations)
            {
                var project = _context
                    .Projects
                    .Where(dbProject => dbProject.ProjectId == item.ProjectId)
                    .SingleOrDefault();

                UserProjects.Add(new
                {
                    projectId = project.ProjectId,
                    projectName = project.ProjectName,
                    isActive = item.Active
                });
            }

            return Ok(UserProjects);
        }

            User FindUserWithSecretKey(string userSecretKey)
        {
            var foundUser = _context
            .Users
            .Where(dbUser => dbUser.SecretKey == userSecretKey)
            .SingleOrDefault();

            if(userSecretKey.Length != 64)
            {
                foundUser = null;
            }

            return foundUser;
        }

    }
}
