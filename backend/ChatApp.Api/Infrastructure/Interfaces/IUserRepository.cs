using ChatApp.Api.Models;

namespace ChatApp.Api.Infrastructure.Interfaces;

public interface IUserRepository
{
    void Add(string connectionId, ChatUser user);

    ChatUser? GetByConnectionId(string connectionId);

    void Remove(string connectionId);
}
