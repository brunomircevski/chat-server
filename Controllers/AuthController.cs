using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Controllers;

[ApiController]
[Route("api/auth")]
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
            request.publicKey
        );

        users.Add(user);

        return Ok(new
        {
            message = $"User {request.username} successfully registered.",
            username = user.username,
            uuid = user.uuid
        });
    }

    [HttpPost("login")]
    public ActionResult<Object> Login(UserLoginDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        User? user = users.Find(x => x.username == request.username);

        if (user is null)
            return NotFound();

        //Verify the signature
        bool isSignatureValid = false;

        try
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(user.publicKey);

            byte[] signatureBytes = Convert.FromBase64String(request.signature);
            byte[] messageBytes = Encoding.UTF8.GetBytes(request.username);

            isSignatureValid = rsa.VerifyData(messageBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }
        catch {}

        if (!isSignatureValid)
            return NotFound();

        System.Console.WriteLine(request.signature);

        string jwt = CreateToken(user);

        return Ok(new { token = jwt });
    }

    [HttpGet("user-info"), Authorize]
    public ActionResult<User> UserInfo()
    {
        User? user = getUser();
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpGet("server-info")]
    public ActionResult<Object> ServerInfo()
    {
        var assembly = Assembly.GetEntryAssembly();
        var attribute = assembly == null ? null : assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        var version = attribute == null ? null : attribute.InformationalVersion;

        return Ok(new { version = version, canRegister = true });
    }

    private User? getUser()
    {
        Claim? uuidClaim = Request.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
        if (uuidClaim is null) return null;
        return users.Where(x => x.uuid == uuidClaim.Value).FirstOrDefault();
    }

    private string CreateToken(User user)
    {
        List<Claim> claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.username),
            new Claim(ClaimTypes.NameIdentifier, user.uuid)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
