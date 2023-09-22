using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Chat.DataAccess;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Controllers;

[ApiController]
[Route("api/message")]
public class MessageController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IDB DB;

    public MessageController(IConfiguration configuration, IDB DB)
    {
        this.configuration = configuration;
        this.DB = DB;
    }

    [HttpPost("")]
    public ActionResult<Object> SendMessage(MessageDto messageDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        Channel channel = DB.Channels.Where(x => x.accessKey == messageDto.channelAccessKey).FirstOrDefault();

        if (channel is null)
            return NotFound();

        Message message = new Message() { content = messageDto.content, channel = channel };

        DB.Messages.Add(message);
        DB.SaveChanges();

        return Ok(new
        {
            message = "Message sent successfully",
        });
    }

    [HttpGet("")]
    public ActionResult<Object> GetMessages(string accessKey, int number = 100, string olderThan = null)
    {
        if (number < 1 || number > 100) number = 100;

        DateTime olderThanDate = DateTime.Now;

        if (olderThan is not null)
        {
            olderThanDate = DB.Messages
            .Include(x => x.channel)
            .Where(x => x.channel.accessKey == accessKey)
            .Where(x => x.uuid == olderThan)
            .Select(x => x.dateCreated)
            .FirstOrDefault();
        }

        var messages = DB.Messages
        .Include(x => x.channel)
        .Where(x => x.channel.accessKey == accessKey)
        .Where(x => x.dateCreated < olderThanDate)
        .OrderByDescending(x => x.dateCreated)
        .Take(number)
        .Select(x => new MessageGetDto()
        {
            uuid = x.uuid,
            content = x.content,
            date = x.dateCreated
        });

        return Ok(new
        {
            count = messages.Count(),
            olderThan = olderThan,
            olderThanDate = olderThanDate,
            messages = messages
        });
    }

}
