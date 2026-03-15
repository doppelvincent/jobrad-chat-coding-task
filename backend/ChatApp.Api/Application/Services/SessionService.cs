using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Services;

public class SessionService(ISessionRepository sessionRepository, ILogger<SessionService> logger) : ISessionService
{
    public ChatSession CreateSession(ChatUser customer)
    {
        var session = new ChatSession { Customer = customer };
        sessionRepository.Add(session);

        logger.LogInformation("Session {SessionId} created for customer '{CustomerName}'", session.Id, customer.Name);

        return session;
    }

    public ChatSession? GetSession(string sessionId) =>
        sessionRepository.GetById(sessionId);

    public IReadOnlyList<ChatSession> GetWaitingSessions() =>
        sessionRepository.GetWaiting();

    public ChatSession AgentJoinSession(string sessionId, ChatUser agent)
    {
        var session = sessionRepository.GetById(sessionId)
            ?? throw new InvalidOperationException($"Session '{sessionId}' not found.");

        session.AssignAgent(agent);
        logger.LogInformation("Agent '{AgentName}' joined session {SessionId}", agent.Name, sessionId);

        return session;
    }

    public ChatSession? CloseSession(string sessionId)
    {
        var session = sessionRepository.GetById(sessionId);
        if (session is null)
            return null;

        session.Close();
        logger.LogInformation("Session {SessionId} closed", sessionId);

        return session;
    }
}
