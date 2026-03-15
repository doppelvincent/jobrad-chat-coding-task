using ChatApp.Api.Models.Enums;

namespace ChatApp.Api.Models;

public class ChatSession
{
    public string Id { get; init; } = Guid.NewGuid().ToString();

    public required ChatUser Customer { get; init; }

    public ChatUser? Agent { get; private set; }

    public ESessionStatus Status { get; private set; } = ESessionStatus.Waiting;

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime? ClosedAt { get; private set; }

    public List<Message> Messages { get; } = [];

    public void AssignAgent(ChatUser agent)
    {
        Agent = agent;
        Status = ESessionStatus.Active;
    }

    public void AddMessage(Message message) => Messages.Add(message);

    public void Close()
    {
        Status = ESessionStatus.Closed;
        ClosedAt = DateTime.UtcNow;
    }
}
