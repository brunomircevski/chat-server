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
        String accessKey = Shared.getRandomString(128);

        Channel channel = new Channel() { accessKey = accessKey };

        DB.Channels.Add(channel);
        DB.SaveChanges();

        return Ok(new
        {
            message = "Channel successfully created.",
            accessKey = accessKey
        });
    }

}
