using System.Text.Json.Serialization;
using ChatApp.Api.Application.Interfaces;
using ChatApp.Api.Application.Services;
using ChatApp.Api.Hubs;
using ChatApp.Api.Hubs.Interfaces;
using ChatApp.Api.Infrastructure.Interfaces;
using ChatApp.Api.Infrastructure.Repositories;

namespace ChatApp.Api;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        var enumConverter = new JsonStringEnumConverter();

        services.AddControllers()
            .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(enumConverter));

        services.AddSignalR()
            .AddJsonProtocol(o => o.PayloadSerializerOptions.Converters.Add(enumConverter));

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
                    ?? ["http://localhost:5173"];

                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .AllowCredentials();
            });
        });
        
        services.AddSingleton<IMessageRepository, InMemoryMessageRepository>();
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddSingleton<ISessionRepository, InMemorySessionRepository>();

        services.AddSingleton<IChatService, ChatService>();
        services.AddSingleton<IUserService, UserService>();
        services.AddSingleton<ISessionService, SessionService>();
    }

    public static void Configure(WebApplication app)
    {
        app.UseCors();
        app.UseAuthorization();
        app.MapHub<ChatHub>("/chat-hub");
        app.MapControllers();
    }
}
