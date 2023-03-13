using Microsoft.AspNetCore.Mvc;
using OwlreportAPI.Data;
using OwlreportAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    //inject context
    private readonly DataContext _dataContext;
    public AuthController(DataContext context)
    {
        _dataContext = context;
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        User user = new User()
        {
            Username = model.Username,
            Password = model.Password
        };

        //Checking for matching user credentials
        var foundUser = _dataContext
            .Users
            .Where(dbUser => dbUser.Username == user.Username && dbUser.Password == user.Password)
            .SingleOrDefault();

        if (foundUser is null)
        {
            return BadRequest(new { message = "Fel Användarnamn eller Lösenord" });
        }
        return Ok();

    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

