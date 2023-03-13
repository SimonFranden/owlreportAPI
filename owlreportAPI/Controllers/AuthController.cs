using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginModel model)
    {
        if (IsValidUser(model.Username, model.Password))
        {
            // Authentication successful
            return Ok(new { message = "Authentication successful" });
        }
        else
        {
            // Authentication failed
            return Unauthorized(new { message = "Invalid username or password" });
        }
    }

    private bool IsValidUser(string username, string password)
    {
        // TODO: Retrieve the credentials from a secure source
        return username == "username" && password == "password";
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}

