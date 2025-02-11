using System.Reflection;
using Generic.Mediator.DependencyInjectionExtensions;
using TicTacToe.AuthService.Options;
using TicTacToe.AuthService.Extensions;
using Migrator = TicTacToe.AuthService.DataAccess.Migrator;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);

builder.Services.AddDbContext(
    builder.Configuration.GetSection("Database").Get<DbOptions>()!);

builder.Services.AddMasstransitRabbitMq(
    builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>()!);

builder.Services.AddMediator(Assembly.GetExecutingAssembly());

builder.Services.AddCors("Frontend", builder.Configuration.GetSection("Frontend")["Url"] 
                                     ?? throw new InvalidOperationException("Frontend:Url is missing"));

var app = builder.Build();

app.UseCors("Frontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await Migrator.MigrateAsync(app.Services);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();