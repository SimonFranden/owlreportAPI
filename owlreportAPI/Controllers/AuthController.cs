using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OwlreportAPI.Data;
using OwlreportAPI.Models;
using System.Diagnostics.Metrics;

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

        //Generate new secret key for the user
        foundUser.SecretKey = SecretKeyGen();
        _dataContext.Update(foundUser);
        _dataContext.SaveChangesAsync();

        return Ok(new { 
            userId = foundUser.Id,
            username = foundUser.Username,
            fName = foundUser.FName,
            lName = foundUser.LName,
            secretKey = foundUser.SecretKey
        });      
    }

    string SecretKeyGen()
    {
        string str = "";
        int randValue;
        char letter;
        Random rand = new Random();

        for (int i = 0; i < 64; i++)
        {
            randValue = rand.Next(0, 26);
            letter = Convert.ToChar(randValue + 65);
            str = str + letter;
        }
        return str;
    }
}

public class LoginModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}


