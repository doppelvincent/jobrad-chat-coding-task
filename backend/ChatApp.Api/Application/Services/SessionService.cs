using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs;
using ChatApp.Api.Hubs.Models;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;
using ChatApp.Api.Models.Enums;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Application.Services;

public class SessionService(IHubContext<ChatHub> hubContext, ISessionRepository sessionRepository, ILogger<SessionService> logger) : ISessionService
{
    public ChatSession CreateSession(ChatUser user)
    {
        var session = new ChatSession();
        
        if (user.Role == EUserRoles.Agent)
        {
            session.AssignAgent(user);
        }
        else
        {
            session.AssignCustomer(user);
        }

        sessionRepository.Add(session);

        logger.LogInformation("Session {SessionId} created  '{UserName}'", session.Id, user.Name);

        return session;
    }

    public ChatSession? GetSession(string sessionId) =>
        sessionRepository.GetById(sessionId);

    public IReadOnlyList<ChatSession> GetAll() =>
        sessionRepository.GetAll();

    public ChatSession JoinSession(string sessionId, ChatUser user)
    {
        var session = sessionRepository.GetById(sessionId)
            ?? throw new InvalidOperationException($"Session '{sessionId}' not found.");
        
        if (user.Role == EUserRoles.Agent)
        {
            session.AssignAgent(user);
            hubContext.Clients.Group(sessionId).SendAsync(EventTypes.AgentJoin, session);
        }
        else
        {
            session.AssignCustomer(user);
        }

        logger.LogInformation("'{UserName}' joined session {SessionId}", user.Name, sessionId);

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
