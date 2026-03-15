namespace ChatApp.Api.Controllers.Requests;

public class SendMessageRequest
{
    public required string UserId { get; init; }

    public required string Content { get; init; }
}