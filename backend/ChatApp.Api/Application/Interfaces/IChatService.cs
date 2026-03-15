using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Interfaces;

public interface IChatService
{
    IReadOnlyList<Message> GetMessagesBySessionId(string sessionId);

    Task SendMessage(Message message);
}