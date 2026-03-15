using ChatApp.Api.Models;

namespace ChatApp.Api.Application.Interfaces;

public interface IChatService
{
    void SendMessage(Message message);
}