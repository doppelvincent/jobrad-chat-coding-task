using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Controllers.Requests;
using ChatApp.Api.Hubs;
using ChatApp.Api.Hubs.Models;
using ChatApp.Api.Models;
using ChatApp.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(
    IChatService chatService, 
    ISessionService sessionService, 
    IUserService userService
    ) : ControllerBase
{
    [HttpGet("{sesionId}/chat-history")]
    public async Task<IActionResult> GetChatHistory(string sessionId)
    {
        IReadOnlyList<Message> messages = chatService.GetMessagesBySessionId(sessionId);

        return Ok(messages);
    }
    
    [HttpPost("{sessionId}/send-message")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest apiRequest, string sessionId)
    {
        var user = userService.GetByUserId(apiRequest.UserId);
        var existingSession = sessionService.GetSession(sessionId);
        
        if (user == null  || existingSession == null)
        {
            return Unauthorized();
        }
        
        if (string.IsNullOrWhiteSpace(apiRequest.Content))
        {
            return BadRequest();
        }
        var message = new Message()
        {
            Content = apiRequest.Content,
            SessionId = sessionId,
            Sender = user,
        };

        await chatService.SendMessage(message);

        return Ok();
    }
}
