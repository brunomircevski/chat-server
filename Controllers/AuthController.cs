using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration configuration;

    public AuthController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    private static List<User> users = new List<User>(); // TO BE REMOVED

    [HttpPost("register")]
    public ActionResult<User> Register(UserRegisterDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        User user = new User(
            Guid.NewGuid().ToString("N"),
            request.username,
            request.challenge,
            request.answer,
            request.publicKey,
            request.encryptedPrivateKey
        );

        users.Add(user);

        return Ok(user);
    }

    [HttpGet("challenge/{username}")]
    public ActionResult<Object> GetChallengeByUsername(string username)
    {
        if (String.IsNullOrEmpty(username)) return BadRequest();
        if (username.Length < 4) return BadRequest();

        User? user = users.Find(x => x.username == username);

        if (user is null) return NotFound();

        return Ok(new { challenge = user.challenge });
    }

    [HttpPost("login")]
    public ActionResult<Object> Login(UserLoginDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        User? user = users.Find(x => x.username == request.username && x.answer == request.answer);

        if (user is null) return NotFound();

        string jwt = CreateToken(user);

        return Ok(new { token = jwt });
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.username),
            new Claim(ClaimTypes.Sid, user.uuid)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(user.tokenExpireDays),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }

}
