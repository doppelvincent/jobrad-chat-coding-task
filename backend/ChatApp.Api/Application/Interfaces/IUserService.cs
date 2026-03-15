using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Interfaces;

public interface IUserService
{
    ChatUser RegisterCustomer(string connectionId, string userName);

    ChatUser RegisterAgent(string connectionId, string agentName);

    ChatUser? GetUser(string connectionId);

    void RemoveUser(string connectionId);
}
