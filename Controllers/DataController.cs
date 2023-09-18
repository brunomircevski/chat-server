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
[Route("api/data")]
public class DataController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IDB DB;

    public DataController(IConfiguration configuration, IDB DB)
    {
        this.configuration = configuration;
        this.DB = DB;
    }

    [HttpPost("invites")]
    public ActionResult<Object> UpdateInvitesData(CustomDataDto request)
    {
        string uuid = getUserUUID();
        User user = DB.Users.Where(x => x.uuid == uuid).FirstOrDefault();

        if(user is null) return BadRequest();

        user.encryptedInvitesData = request.data;

        DB.SaveChanges();

        return Ok(new
        {
            message = "Data saved"
        });
    }

    [HttpGet("invites")]
    public ActionResult<Object> GetInvitesData()
    {
        string uuid = getUserUUID();
        User user = DB.Users.Where(x => x.uuid == uuid).FirstOrDefault();

        if(user is null) return NotFound();

        return Ok(new { 
            data = user.encryptedInvitesData
        });
    }

    [HttpGet("received-invites")]
    public ActionResult<Object> GetMyInvites()
    {        
        string uuid = getUserUUID();
        User user = DB.Users.Where(x => x.uuid == uuid).FirstOrDefault();

        if (user is null)
            return NotFound();

        List<Invite> invites = DB.Invites.Where(x => x.user == user).ToList();

        if (invites is null)
            return NoContent();

        List<InviteDto> invitesDto = new List<InviteDto>();

        foreach (Invite inv in invites)
        {   
            invitesDto.Add(new InviteDto() { username = null, content = inv.content, accessKey = inv.accessKey, encryptedKey = inv.encryptedKey});
        }

        return Ok(invitesDto);
    }

    [HttpPost("user")]
    public ActionResult<Object> UpdateUserData(CustomDataDto request)
    {
        string uuid = getUserUUID();
        User user = DB.Users.Where(x => x.uuid == uuid).FirstOrDefault();

        if(user is null) return BadRequest();

        user.encryptedUserData = request.data;

        DB.SaveChanges();

        return Ok(new
        {
            message = "Data saved"
        });
    }

    [HttpGet("user")]
    public ActionResult<Object> GetUserData()
    {
        string uuid = getUserUUID();
        User user = DB.Users.Where(x => x.uuid == uuid).FirstOrDefault();

        if(user is null) return NotFound();

        return Ok(new { 
            data = user.encryptedUserData
        });
    }

    private string getUserUUID()
    {
        Claim uuidClaim = Request.HttpContext.User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
        if (uuidClaim is null) return null;
        return uuidClaim.Value;
    }
}
