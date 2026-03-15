using ChatApp.Api.Models.Enums;

namespace ChatApp.Api.Models;

public class Message
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public required string SessionId { get; init; }

    public required ChatUser Sender { get; init; }

    public required string Content { get; init; }

    public DateTime SentAt { get; } = DateTime.UtcNow;
}
