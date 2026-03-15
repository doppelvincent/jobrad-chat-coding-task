using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Interfaces;

public interface ISessionService
{
    ChatSession CreateSession(ChatUser user);

    ChatSession? GetSession(string sessionId);

    IReadOnlyList<ChatSession> GetAll();

    ChatSession JoinSession(string sessionId, ChatUser user);

    ChatSession? CloseSession(string sessionId);
}
