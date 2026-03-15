using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;
using ChatApp.Api.Models.Enums;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Application.Services;

public class UserService(IHubContext<ChatHub> hubContext, IUserRepository userRepository, ILogger<UserService> logger) : IUserService
{
    public ChatUser? GetByUserId(string userId) =>
        userRepository.GetByUserId(userId);

    public void RegisterExistingUser(string connectionId, ChatUser user, string sessionId)
    {
        userRepository.Add(connectionId, user);
        hubContext.Groups.AddToGroupAsync(connectionId, sessionId);

        logger.LogInformation("User '{UserName}' registered on connection {ConnectionId}", user.Name, connectionId);
    }

    public void RemoveUser(string connectionId)
    {
        userRepository.Remove(connectionId);

        logger.LogInformation("User on connection {ConnectionId} disconnected", connectionId);
    }
}
