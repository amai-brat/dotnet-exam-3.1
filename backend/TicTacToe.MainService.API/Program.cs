using System.Collections.Concurrent;
using TicTacToe.MainService;
using TicTacToe.MainService.Consts;
using TicTacToe.MainService.Hubs;
using TicTacToe.MainService.Infrastructure;
using TicTacToe.MainService.Infrastructure.Data;
using TicTacToe.MainService.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddCorsPolicy(builder.Configuration.GetSection("Frontend")["Url"] 
                               ?? throw new InvalidOperationException("Frontend:Url not configured"));
builder.Services.AddJwtAuthentication(
    builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!);
builder.Services.AddAuthorization();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMassTransitRabbitMq(builder.Configuration);
builder.Services.AddKeyedSingleton<ConcurrentDictionary<int, Room>>(KeyedServices.GameRooms);
builder.Services.AddHttpClient("Rating", httpClient =>
{
    httpClient.BaseAddress = new Uri(builder.Configuration.GetSection("RatingService")["Url"]
                                     ?? throw new InvalidOperationException());
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await Migrator.MigrateAsync(app.Services);

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("SPA");
app.MapHub<GameHub>("/game");
app.MapControllers();
app.Run();
