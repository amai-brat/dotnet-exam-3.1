using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TicTacToe.MainService.Infrastructure.Data;

public static class Migrator
{
    public static async Task MigrateAsync(IServiceProvider serviceProvider)
    {
        await using(var scope = serviceProvider.CreateAsyncScope())
        {
            await Task.Delay(1000);

            var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
            if ((await dbContext!.Database.GetPendingMigrationsAsync()).Any())
            {
                await dbContext.Database.MigrateAsync();
            }   
        }
    }
}