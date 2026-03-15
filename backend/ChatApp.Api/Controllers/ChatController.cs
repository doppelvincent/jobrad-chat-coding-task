using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Hubs;
using ChatApp.Api.Hubs.Models;
using ChatApp.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController() : ControllerBase
{
    [HttpPost("{sessionId}/close")]
    public async Task<IActionResult> SendMessage([FromBody] object message)
    {
        return Ok();
    }
}
