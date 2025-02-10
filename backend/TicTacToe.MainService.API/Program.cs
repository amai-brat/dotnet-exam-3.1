using TicTacToe.MainService;
using TicTacToe.MainService.Infrastructure;
using TicTacToe.MainService.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMassTransitRabbitMq(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await Migrator.MigrateAsync(app.Services);

app.MapControllers();
app.Run();