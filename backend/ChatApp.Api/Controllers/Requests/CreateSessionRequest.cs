namespace ChatApp.Api.Controllers.Requests;

public class CreateSessionRequest
{
    public required string Name { get; init; }
    
    public required bool IsAgent { get; init; }

    public required string ConnectionId { get; init; }
}