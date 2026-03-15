using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs.Interfaces;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Services;

public class ChatService(IChatHub chatHub, IMessageRepository messageRepository, ISessionRepository sessionRepository) : IChatService
{
    public void SendMessage(Message message, string sessionId)
    {
        var session = sessionRepository.GetById(sessionId) ?? throw new InvalidOperationException($"Session '{sessionId}' not found.");

        session.AddMessage(message);
        messageRepository.Add(message);

        chatHub.SendMessage(sessionId, message.Content);
    }
}