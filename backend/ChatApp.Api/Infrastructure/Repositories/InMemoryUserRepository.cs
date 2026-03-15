using System.Collections.Concurrent;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Models;

namespace ChatApp.Api.Infrastructure.Repositories;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<string, ChatUser> _users = new();

    public void Add(string connectionId, ChatUser user) =>
        _users[connectionId] = user;

    public ChatUser? GetByConnectionId(string connectionId) =>
        _users.TryGetValue(connectionId, out var user) ? user : null;

    public void Remove(string connectionId) =>
        _users.TryRemove(connectionId, out _);
}
