using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Controllers.Requests;
using ChatApp.Api.Hubs;
using ChatApp.Api.Models;
using ChatApp.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatSessionController(ISessionService sessionService) : ControllerBase
{
    [HttpPost("create")]
    public IActionResult CreateSession([FromBody] CreateSessionRequest apiRequest)
    {
        var user = new ChatUser
        {
            Name = apiRequest.Name,
            Role = apiRequest.IsAgent ? EUserRoles.Agent : EUserRoles.Customer,
        };

        var session = sessionService.CreateSession(user);

        return Ok(session);
    }
    
    [HttpGet]
    public IActionResult GetSessions()
    {
        return Ok(sessionService.GetWaitingSessions());
    }

    [HttpGet("{sessionId}")]
    public IActionResult GetSession(string sessionId)
    {
        var session = sessionService.GetSession(sessionId);

        if (session is null)
            return NotFound(new { message = $"Session '{sessionId}' not found." });

        return Ok(session);
    }
    
    [HttpPost("{sessionId}/join")]
    public IActionResult JoinSession([FromBody] JoinSessionRequest apiRequest, string sessionId)
    {
        var user = new ChatUser
        {
            Name = apiRequest.Name,
            Role = apiRequest.IsAgent ? EUserRoles.Agent : EUserRoles.Customer,
        };

        var updatedSession = sessionService.JoinSession(sessionId, user);
        return Ok(updatedSession);
    }

    [HttpPost("{sessionId}/close")]
    public IActionResult CloseSession(string sessionId)
    {
        var session = sessionService.CloseSession(sessionId);

        if (session is null)
        {
            return NotFound(new { message = $"Session '{sessionId}' not found." });
        }

        return Ok(session);
    }
}
