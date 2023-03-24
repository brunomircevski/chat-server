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
    public ActionResult<Object> Register(UserRegisterDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (users.Find(x => x.username == request.username) is not null)
            return BadRequest(new { message = $"User {request.username} already exists." });

        User user = new User(
            Guid.NewGuid().ToString("N"),
            request.username,
            BCrypt.Net.BCrypt.HashPassword(request.password),
            request.publicKey,
            request.encryptedPrivateKey
        );

        users.Add(user);

        return Ok(new { message = $"User {request.username} successfully registered." });
    }

    [HttpPost("login")]
    public ActionResult<Object> Login(UserLoginDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        User? user = users.Find(x => x.username == request.username);

        if (user is null)
            return NotFound();

        if (!BCrypt.Net.BCrypt.Verify(request.password, user.passwordHash))
            return NotFound();

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
