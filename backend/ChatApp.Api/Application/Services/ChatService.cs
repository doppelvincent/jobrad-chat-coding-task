using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs;
using ChatApp.Api.Hubs.Models;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Application.Services;

public class ChatService(IHubContext<ChatHub> hubContext, IMessageRepository messageRepository, ISessionRepository sessionRepository) : IChatService
{
    public IReadOnlyList<Message> GetMessagesBySessionId(string sessionId) =>
        messageRepository.GetBySessionId(sessionId);

    public async Task SendMessage(Message message)
    {
        var session = sessionRepository.GetById(message.SessionId)
            ?? throw new InvalidOperationException($"Session '{message.SessionId}' not found.");

        session.AddMessage(message);
        messageRepository.Add(message);

        await hubContext.Clients.Group(message.SessionId).SendAsync(EventTypes.ReceiveMessage, message);
    }
}