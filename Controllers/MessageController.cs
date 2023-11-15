using Chat.DataAccess;
using Chat.Hubs;
using Chat.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Chat.Controllers;

[ApiController]
[Route("api/message")]
public class MessageController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IHubContext<ChatHub> hubContext;
    private readonly IDB DB;

    public MessageController(IConfiguration configuration, IHubContext<ChatHub> hubContext, IDB DB)
    {
        this.configuration = configuration;
        this.DB = DB;
        this.hubContext = hubContext;
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

        SendToConnectedClients(channel.uuid, message);

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

        Channel channel = DB.Channels.Where(x => x.accessKey == accessKey).FirstOrDefault();
        if(channel == null) return NotFound();

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

    private void SendToConnectedClients(string channelUUID, Message message) {
        MessageGetDto messageDto = new MessageGetDto()
        {
            uuid = message.uuid,
            content = message.content,
            date = message.dateCreated
        };

        hubContext.Clients.Group(channelUUID).SendAsync("ReceivedMessage", messageDto);
    }

}
