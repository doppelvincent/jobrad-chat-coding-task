namespace ChatApp.Api.Controllers.Requests;

public class JoinSessionRequest
{
    public required string Name { get; init; }
    
    public required bool IsAgent { get; init; }

    public required string ConnectionId { get; init; }
}