using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs.Interfaces;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Services;

public class ChatService(IChatHub chatHub, IMessageRepository messageRepository, ISessionRepository sessionRepository) : IChatService
{
    public void SendMessage(Message message)
    {
        var session = sessionRepository.GetById(message.SessionId) ?? throw new InvalidOperationException($"Session '{message.SessionId}' not found.");

        session.AddMessage(message);
        messageRepository.Add(message);

        chatHub.SendMessage(message.SessionId, message);
    }
}