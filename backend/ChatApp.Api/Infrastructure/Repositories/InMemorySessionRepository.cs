using System.Collections.Concurrent;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;
using ChatApp.Api.Models.Enums;

namespace ChatApp.Api.Infrastructure.Repositories;

public class InMemorySessionRepository : ISessionRepository
{
    private readonly ConcurrentDictionary<string, ChatSession> _sessions = new();

    public void Add(ChatSession session) =>
        _sessions[session.Id] = session;

    public ChatSession? GetById(string sessionId) =>
        _sessions.TryGetValue(sessionId, out var session) ? session : null;

    public IReadOnlyList<ChatSession> GetAll() =>
        _sessions.Values.ToList().AsReadOnly();
}
