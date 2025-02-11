using Microsoft.EntityFrameworkCore;

namespace TicTacToe.AuthService.DataAccess;

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