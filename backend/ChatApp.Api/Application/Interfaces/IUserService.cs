using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Interfaces;

public interface IUserService
{
    ChatUser? GetByUserId(string userId);

    void RegisterExistingUser(string connectionId, ChatUser user, string sessionId);

    void RemoveUser(string connectionId);
}
