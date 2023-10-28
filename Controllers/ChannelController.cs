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
[Route("api/channel")]
public class ChannelController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IDB DB;

    public ChannelController(IConfiguration configuration, IDB DB)
    {
        this.configuration = configuration;
        this.DB = DB;
    }

    [HttpPost(""), Authorize]
    public ActionResult<Object> CreateChannel()
    {
        String accessKey = Shared.GetRandomString(128);

        Channel channel = new Channel() { accessKey = accessKey };

        DB.Channels.Add(channel);
        DB.SaveChanges();

        return Ok(new
        {
            message = "Channel successfully created.",
            uuid = channel.uuid,
            accessKey = accessKey
        });
    }

    [HttpDelete("")]
    public ActionResult<Object> RemoveChannel(string accessKey)
    {        
        Channel channel = DB.Channels.Where(x => x.accessKey == accessKey).FirstOrDefault();

        if (channel is null)
            return NotFound();
        
        DB.Remove(channel);
        DB.SaveChanges();

        return Ok(new { message = "Channel removed" });
    }

    [HttpGet("status")]
    public ActionResult<Object> CheckChannelStatus(string accessKey)
    {
        Channel channel = DB.Channels.Where(x => x.accessKey == accessKey).FirstOrDefault();

        if (channel is null)
            return NotFound();

        return Ok(new
        {
            active = channel.active
        });
    }

    [HttpPost("activate")]
    public ActionResult<Object> ActivateChannel(string accessKey)
    {
        Channel channel = DB.Channels.Where(x => x.accessKey == accessKey).FirstOrDefault();

        if (channel is null)
            return NotFound();
        
        if (channel.active)
            return Ok(new { message = "Channel is already active" });

        channel.active = true;
        DB.SaveChanges();

        return Ok(new { message = "Channel activated" });
    }

}
