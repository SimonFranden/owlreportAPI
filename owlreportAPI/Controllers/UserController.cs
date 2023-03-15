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
