using ChatApp.Api.Models.Enums;

namespace ChatApp.Api.Models;

public class ChatUser
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public string Name { get; init; } = string.Empty;

    public EUserRoles Role { get; init; }
}
