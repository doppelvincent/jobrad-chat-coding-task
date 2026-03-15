using ChatApp.Api.Models;

namespace ChatApp.Api.Hubs.Interfaces;

public interface IChatHub
{
    Task SendMessage(string sessionId, Message message);
}