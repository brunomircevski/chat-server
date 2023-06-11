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
[Route("api/invite")]
public class InviteController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IDB DB;

    public InviteController(IConfiguration configuration, IDB DB)
    {
        this.configuration = configuration;
        this.DB = DB;
    }

    [HttpPost("")]
    public ActionResult<Object> CreateInvite(InviteDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        User user = DB.Users.Where(x => x.username == request.username).FirstOrDefault();

        if (user is null)
            return NotFound(new { message = $"User {request.username} not found." });

        if (!user.acceptsInvites || user.Invites?.Count >= 10)
            return BadRequest(new { message = $"User {request.username} does not accept invites or has to many invites." });

        String accessKey = Shared.getRandomString(128);

        Invite invite = new Invite() { user = user, content = request.content, accessKey = accessKey, encryptedKey = request.encryptedKey};

        DB.Invites.Add(invite);
        DB.SaveChanges();

        return Ok(new { 
            message = $"Invite to {request.username} successfully send.", 
            accessKey = accessKey
        });
    }

    [HttpGet("")]
    public ActionResult<Object> CheckInvite(string accessKey)
    {        
        Invite invite = DB.Invites.Where(x => x.accessKey == accessKey).FirstOrDefault();

        if (invite is null)
            return NotFound();

        return Ok(new { message = "Pending" });
    }

    [HttpDelete("")]
    public ActionResult<Object> CancelInvite(string inviteAccessKey)
    {        
        Invite invite = DB.Invites.Where(x => x.accessKey == inviteAccessKey).FirstOrDefault();

        if (invite is null)
            return NotFound();
        
        DB.Remove(invite);
        DB.SaveChanges();

        return Ok(new { message = "Invite canceled" });
    }

    [HttpGet("public-key")]
    public ActionResult<Object> GetPublicKey(string username)
    {        
        User user = DB.Users.Where(x => x.username == username).FirstOrDefault();

        if (user is null)
            return NotFound();

        return Ok(new { username = user.username, publicKey = user.publicKey });
    }

}
