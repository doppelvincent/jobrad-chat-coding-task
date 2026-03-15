using ChatApp.Api.Models;

namespace ChatApp.Api.Hubs.Interfaces;

public interface IChatHub
{
    Task RegisterCustomer(string userName);

    Task RegisterAgent(string agentName);

    Task CreateSession();

    Task JoinSession(string sessionId);

    Task SendMessage(string sessionId, Message message);
}