using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Interfaces;

public interface IMessageService
{
    IReadOnlyList<Message> GetMessages(string sessionId);
    
    IReadOnlyList<Message> Update(string sessionId);
    
}