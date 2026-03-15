using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs;
using ChatApp.Api.Hubs.Models;
using ChatApp.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatSessionController(
    ISessionService sessionService,
    IHubContext<ChatHub> hubContext) : ControllerBase
{
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
    
    [HttpPost("{sessionId}/close")]
    public async Task<IActionResult> JoinSession(string sessionId)
    {
        return Ok();
    }

    [HttpPost("{sessionId}/close")]
    public async Task<IActionResult> CloseSession(string sessionId)
    {
        var session = sessionService.CloseSession(sessionId);

        if (session is null)
            return NotFound(new { message = $"Session '{sessionId}' not found." });

        await hubContext.Clients.Group(sessionId)
            .SendAsync(EventTypes.SessionClosed, sessionId);

        return Ok(session);
    }
}
