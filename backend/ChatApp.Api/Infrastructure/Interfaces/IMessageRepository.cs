using ChatApp.Api.Models;

namespace ChatApp.Api.Infrastructure.Interfaces;

public interface IMessageRepository
{
    void Add(Message message);

    IReadOnlyList<Message> GetBySessionId(string sessionId);
}
