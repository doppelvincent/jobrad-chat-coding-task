using System.Collections.Concurrent;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;

namespace ChatApp.Api.Infrastructure.Repositories;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly ConcurrentDictionary<string, List<Message>> _messagesBySession = new();

    public void Add(Message message)
    {
        var messages = _messagesBySession.GetOrAdd(message.SessionId, _ => []);

        lock (messages)
        {
            messages.Add(message);
        }
    }

    public IReadOnlyList<Message> GetBySessionId(string sessionId) =>
        _messagesBySession.TryGetValue(sessionId, out var messages)
            ? messages.AsReadOnly()
            : [];
}
