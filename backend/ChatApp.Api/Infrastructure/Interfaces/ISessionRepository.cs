using ChatApp.Api.Models;

namespace ChatApp.Api.Infrastructure.Interfaces;

public interface ISessionRepository
{
    void Add(ChatSession session);

    ChatSession? GetById(string sessionId);

    IReadOnlyList<ChatSession> GetWaiting();
}
