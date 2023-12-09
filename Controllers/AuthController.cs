using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Chat.DataAccess;
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
    private readonly IDB DB;
    private static Dictionary<string, string> challenges = new();

    public AuthController(IConfiguration configuration, IDB DB)
    {
        this.configuration = configuration;
        this.DB = DB;
    }

    [HttpPost("register")]
    public ActionResult<Object> Register(UserRegisterDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (DB.Users.Where(x => x.username == request.username).FirstOrDefault() is not null)
            return BadRequest(new { message = $"User {request.username} already exists." });

        User user = new User(
            Guid.NewGuid().ToString("N"),
            request.username,
            request.publicKey
        );

        DB.Users.Add(user);
        DB.SaveChanges();

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

        User user = DB.Users.Where(x => x.username == request.username).FirstOrDefault();

        if (user is null)
            return NotFound();

        if(!challenges.ContainsKey(request.username)) 
            return BadRequest(new { message = "You need to get a challenge first" });

        //Verify the signature
        try
        {
            RSA rsa = RSA.Create();
            rsa.ImportFromPem(user.publicKey);

            byte[] signatureBytes = Convert.FromBase64String(request.signature);
            byte[] messageBytes = Encoding.UTF8.GetBytes(challenges[request.username]);

            bool isSignatureValid = rsa.VerifyData(messageBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

            if (!isSignatureValid)
                return NotFound();
        }
        catch {
            return NotFound();
        }

        string jwt = CreateToken(user);
        challenges.Remove(request.username);

        return Ok(new { token = jwt });
    }

    [HttpDelete("account"), Authorize]
    public ActionResult<Object> RemoveUser()
    {        
        User user = getUser();
        
        DB.Remove(user);
        DB.SaveChanges();

        return Ok(new { message = "Account deleted" });
    }

    [HttpGet("user-info"), Authorize]
    public ActionResult<User> UserInfo()
    {
        User user = getUser();
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpGet("challenge")]
    public ActionResult<string> GetChallenge(string username)
    {
        string uuid = DB.Users.Where(x => x.username == username).Select(x => x.uuid).FirstOrDefault();
        if(uuid is null) return NotFound();
        
        if(challenges.ContainsKey(username)) return Ok(new { challenge = challenges[username]});

        string challenge = Shared.GetRandomString(64);
        challenges.Add(username, challenge);
        return Ok(new { challenge = challenge});
    }

    [HttpGet("server-info")]
    public ActionResult<Object> ServerInfo()
    {
        var assembly = Assembly.GetEntryAssembly();
        var attribute = assembly == null ? null : assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
        var version = attribute == null ? null : attribute.InformationalVersion;

        return Ok(new { version = version, canRegister = true, encryptedChatServer = true });
    }

    private User getUser()
    {
        Claim uuidClaim = Request.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
        if (uuidClaim is null) return null;
        return DB.Users.Where(x => x.uuid == uuidClaim.Value).FirstOrDefault();
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
