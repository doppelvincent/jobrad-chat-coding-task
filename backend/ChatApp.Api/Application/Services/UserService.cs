using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;
using ChatApp.Api.Models.Enums;

namespace ChatApp.Api.Application.Services;

public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
{
    public ChatUser RegisterCustomer(string connectionId, string userName)
    {
        var customer = new ChatUser { Name = userName, Role = EUserRoles.Customer };
        userRepository.Add(connectionId, customer);

        logger.LogInformation("Customer '{UserName}' registered on connection {ConnectionId}", userName, connectionId);

        return customer;
    }

    public ChatUser RegisterAgent(string connectionId, string agentName)
    {
        var agent = new ChatUser { Name = agentName, Role = EUserRoles.Agent };
        userRepository.Add(connectionId, agent);

        logger.LogInformation("Agent '{AgentName}' registered on connection {ConnectionId}", agentName, connectionId);

        return agent;
    }

    public ChatUser? GetByConnectionId(string connectionId) =>
        userRepository.GetByConnectionId(connectionId);

    public ChatUser? GetByUserId(string userId) =>
        userRepository.GetByUserId(userId);

    public void RemoveUser(string connectionId)
    {
        userRepository.Remove(connectionId);

        logger.LogInformation("User on connection {ConnectionId} disconnected", connectionId);
    }
}
