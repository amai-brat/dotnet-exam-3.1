using System.Reflection;
using Generic.Mediator.DependencyInjectionExtensions;
using TicTacToe.RatingService.Abstractions.Consumers;
using TicTacToe.RatingService.Consumers;
using TicTacToe.RatingService.Extensions;
using TicTacToe.RatingService.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(
    builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!);
builder.Services.AddAuthorization();

builder.Services.AddMasstransitRabbitMq(
    builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!);

builder.Services.AddDbContext(builder.Configuration);

builder.Services.AddCors("Frontend", "http://localhost:5173");

builder.Services.AddScoped<IMatchEndedConsumer, MatchEndedConsumer>();
builder.Services.AddScoped<IUserRegisteredConsumer, UserRegisteredConsumer>();

builder.Services.AddMediator(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseCors("Frontend");

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();