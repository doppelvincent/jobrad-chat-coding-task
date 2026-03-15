using ChatApp.Api.Models.Enums;

namespace ChatApp.Api.Models;

public class ChatUser
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public required string Name { get; init; }

    public required EUserRoles Role { get; init; }
}
