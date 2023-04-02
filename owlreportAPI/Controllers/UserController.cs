using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            List<User> userList = new();
            userList = await _context.Users.ToListAsync();

            List<PublicUserInfo> publicUserInfoList = new();
            foreach (User user in userList)
            {
                PublicUserInfo newPublicUserInfo = new();
                newPublicUserInfo.Id = user.Id;
                newPublicUserInfo.Username = user.Username;
                newPublicUserInfo.FName = user.FName;
                newPublicUserInfo.LName = user.LName;

                publicUserInfoList.Add(newPublicUserInfo);
            }

            return Ok(publicUserInfoList);
        }

        [HttpGet("{userSecretKey}")]
        public async Task<ActionResult<List<User>>> Get(string userSecretKey)
        {
            var foundUser = FindUserWithSecretKey(userSecretKey);

            if (foundUser == null)
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

        [HttpGet("UserProjects/{userSecretKey}")]
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
            foreach (ProjectUserRelation item in projectUserRelations)
            {
                var project = _context
                    .Projects
                    .Where(dbProject => dbProject.ProjectId == item.ProjectId)
                    .SingleOrDefault();

                if (project != null)
                {
                    UserProjects.Add(new
                    {
                        projectId = project.ProjectId,
                        projectName = project.ProjectName,
                        isActive = item.Active
                    });
                }


            }

            return Ok(UserProjects);
        }

        [HttpPost("CreateProject")]
        public async Task<ActionResult<List<User>>> CreateProject(CreateProjectModel createProjectModel)
        {
            var foundUser = FindUserWithSecretKey(createProjectModel.UserSecretKey);
            if (foundUser == null)
            {
                return BadRequest("User not found");
            }

            Project createdProject = new();
            createdProject.ProjectName = createProjectModel.ProjectName;
            createdProject.ProjectLength = createProjectModel.ProjectLength;
            createdProject.ProjectOwner = foundUser.Id;

            _context.Projects.Add(createdProject);
            await _context.SaveChangesAsync();

            ProjectUserRelation newRelation = new();
            newRelation.ProjectId = _context
            .Projects
            .Where(dbProject => dbProject.ProjectName == createdProject.ProjectName)
            .SingleOrDefault().ProjectId;

            newRelation.UserId = foundUser.Id;
            newRelation.Active = true;

            _context.ProjectUserRelations.Add(newRelation);
            await _context.SaveChangesAsync();
            return Ok("Project Created");
        }

        [HttpPost("EditProject")]
        public async Task<ActionResult<List<User>>> EditProject(EditProjectModel projectUpdates)
        {
            var foundUser = FindUserWithSecretKey(projectUpdates.UserSecretKey);
            if (foundUser == null)
            {
                return BadRequest("User not found");
            }

            var projectToUpdate = _context.Projects.FirstOrDefault(e => e.ProjectId == projectUpdates.ProjectId);

            if (projectToUpdate == null || projectToUpdate.ProjectOwner != foundUser.Id) return BadRequest("Something went wrong");

            projectToUpdate.ProjectName = projectUpdates.ProjectName;
            projectToUpdate.ProjectLength = projectUpdates.ProjectLength;

            await _context.SaveChangesAsync();
            return Ok($"Project{projectToUpdate.ProjectName} updated");
        }

        [HttpPost("DeleteProject")]
        public async Task<ActionResult<List<User>>> DeleteProject(DeleteProjectModel projectInfo)
        {
            var foundUser = FindUserWithSecretKey(projectInfo.UserSecretKey);
            if (foundUser == null)
            {
                return BadRequest("User not found");
            }

            var projectToDelete = _context.Projects.FirstOrDefault(e => e.ProjectId == projectInfo.ProjectId);
            if (projectToDelete.ProjectOwner != foundUser.Id) return BadRequest("User is not the project owner!");

            _context.Projects.Remove(projectToDelete);
            await _context.SaveChangesAsync();
            return Ok("Project deleted");

        }


        [HttpPost("AddUsersToProject")]
        public async Task<ActionResult<List<User>>> AddUsersToProject(EditUsers relationsToAdd)
        {
            var foundUser = FindUserWithSecretKey(relationsToAdd.UserSecretKey);

            if (foundUser == null) return BadRequest("User not found");

            foreach (UserToEdit relationToAdd in relationsToAdd.UserList)
            {
                var projectToValidate = _context.Projects.FirstOrDefault(e => e.ProjectId == relationToAdd.ProjectId);
                if (projectToValidate.ProjectOwner != foundUser.Id) return BadRequest("User is not the project owner!");

                ProjectUserRelation newRelation = new();
                newRelation.ProjectId = relationToAdd.ProjectId;
                newRelation.UserId = relationToAdd.UserId;
                newRelation.Active = true;

                _context.ProjectUserRelations.Add(newRelation);
                await _context.SaveChangesAsync();
            }
            return Ok("Users added");
        }

        [HttpPost("RemoveUsersToProject")]
        public async Task<ActionResult<List<User>>> RemoveUsersToProject(EditUsers relationsToRemove)
        {
            var foundUser = FindUserWithSecretKey(relationsToRemove.UserSecretKey);
            if (foundUser == null) return BadRequest("User not found");

            foreach (UserToEdit relationToRemove in relationsToRemove.UserList)
            {
                var projectToValidate = _context.Projects.FirstOrDefault(e => e.ProjectId == relationToRemove.ProjectId);
                if (projectToValidate.ProjectOwner != foundUser.Id) return BadRequest("User is not the project owner!");

                var theRelationToRemove = _context.ProjectUserRelations.FirstOrDefault(e => e.ProjectId == relationToRemove.ProjectId && e.UserId == relationToRemove.UserId);

                _context.ProjectUserRelations.Remove(theRelationToRemove);
                await _context.SaveChangesAsync();

            }
            return Ok("Users Removed");
        }


        User FindUserWithSecretKey(string userSecretKey)
        {
            var foundUser = _context
            .Users
            .Where(dbUser => dbUser.SecretKey == userSecretKey)
            .SingleOrDefault();

            if (userSecretKey.Length != 64)
            {
                foundUser = null;
            }

            return foundUser;
        }

    }
}