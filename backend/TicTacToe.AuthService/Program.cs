using System.Reflection;
using Generic.Mediator.DependencyInjectionExtensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TicTacToe.AuthService.Options;
using TicTacToe.AuthService.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);
builder.Services.AddValidators();

builder.Services.AddDbContext(
    builder.Configuration.GetSection("Database").Get<DbOptions>()!);

builder.Services.AddMasstransitRabbitMq(
    builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!);

builder.Services.AddMediator(Assembly.GetExecutingAssembly());

builder.Services.AddCors("Frontend", "http://localhost:5173");

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();