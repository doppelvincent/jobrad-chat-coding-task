using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs.Interfaces;
using ChatApp.Api.Hubs.Models;
using ChatApp.Api.Models;
using ChatApp.Api.Models.Enums;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Hubs;

public class ChatHub(IUserService userService, ISessionService sessionService, ILogger<ChatHub> logger) : Hub, IChatHub
{
    public async Task SendMessage(string sessionId, Message message)
    {
        await Clients.Group(sessionId).SendAsync(EventTypes.ReceiveMessage, message);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        if (exception is not null)
            logger.LogWarning(exception, "Connection {ConnectionId} disconnected with error", Context.ConnectionId);

        userService.RemoveUser(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }
}
