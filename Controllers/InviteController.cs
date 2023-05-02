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

[Authorize]
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

    [HttpPost("new")]
    public ActionResult<Object> CreateInvite(InviteDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        User user = DB.Users.Where(x => x.username == request.username).FirstOrDefault();

        if (user is null)
            return NotFound(new { message = $"User {request.username} not found." });

        if (!user.acceptsInvites)
            return BadRequest(new { message = $"User {request.username} does not accept invites." });

        if(user == getUser())
            return BadRequest(new { message = $"You cannot invite yourself." });

        Invite invite = new Invite() { user = user, content = request.content, accessKey = request.accessKey};

        DB.Invites.Add(invite);
        DB.SaveChanges();

        return Ok(new { message = $"Invite to {request.username} successfully send." });
    }

    [HttpGet("status")]
    public ActionResult<Object> CheckInvite(string accessKey)
    {        
        Invite invite = DB.Invites.Where(x => x.accessKey == accessKey).FirstOrDefault();

        if (invite is null)
            return NotFound();

        return Ok(new { message = $"Pending" });
    }

    private User getUser()
    {
        Claim uuidClaim = Request.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
        if (uuidClaim is null) return null;
        return DB.Users.Where(x => x.uuid == uuidClaim.Value).FirstOrDefault();
    }

}
