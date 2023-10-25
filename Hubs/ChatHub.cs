using Chat.DataAccess;
using Chat.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Hubs;

public class ChatHub : Hub
{
    private readonly IDB DB;

    public ChatHub(IDB DB)
    {
        this.DB = DB;
    }

    public async Task JoinRoom(string accessKey) {
        string uuid = DB.Channels.Where(x => x.accessKey == accessKey).Select(x => x.uuid).FirstOrDefault();
        if(uuid is null) {
            await Clients.Caller.SendAsync("JoinRoomError", "Invalid access key or room not found.");
            return;
        }

        await Groups.AddToGroupAsync(Context.ConnectionId, uuid);
        await Clients.Caller.SendAsync("JoinRoomOk", "Successfully joined room");
    }
}
