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

[ApiController, Authorize]
[Route("api/settings")]
public class SettingsController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IDB DB;

    public SettingsController(IConfiguration configuration, IDB DB)
    {
        this.configuration = configuration;
        this.DB = DB;
    }

    [HttpPost("accepts-invites")]
    public ActionResult<Object> SetAcceptsInvites ([FromBody] SingleBoolean acceptsInvites)
    {
        string uuid = getUserUUID();
        User user = DB.Users.Where(x => x.uuid == uuid).FirstOrDefault();

        if(user is null) return NotFound();

        user.acceptsInvites = acceptsInvites.value;

        DB.SaveChanges();

        string noString = "";
        if(!acceptsInvites.value) noString = "not";

        return Ok(new
        {
            message = "Settings updated. You are " + noString + "accepting invites."
        });
    }

    [HttpGet("")]
    public ActionResult<Object> GetSettings()
    {
        string uuid = getUserUUID();
        User user = DB.Users.Where(x => x.uuid == uuid).FirstOrDefault();

        if(user is null) return NotFound();

        return Ok(new {
            user.uuid,
            user.username,
            user.acceptsInvites
        });
    }

    private string getUserUUID()
    {
        Claim uuidClaim = Request.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
        if (uuidClaim is null) return null;
        return uuidClaim.Value;
    }

    public class SingleBoolean
    {
        public bool value { get; set; }
    }
}
