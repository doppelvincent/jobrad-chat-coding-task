using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Interfaces;

public interface ISessionService
{
    ChatSession CreateSession(ChatUser customer);

    ChatSession? GetSession(string sessionId);

    IReadOnlyList<ChatSession> GetWaitingSessions();

    ChatSession AgentJoinSession(string sessionId, ChatUser agent);

    Message AddMessage(string sessionId, ChatUser sender, string content);

    ChatSession? CloseSession(string sessionId);
}
