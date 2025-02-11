

using TicTacToe.MainService.Application.Repositories;

namespace TicTacToe.MainService.BackgroundServices;

public class ClosedGamesCleanerBackgroundService(
    IServiceProvider serviceProvider
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await using (var scope = serviceProvider.CreateAsyncScope())
            {
                var cleaner = scope.ServiceProvider.GetService<ClosedGamesCleanerService>()!;
                await cleaner.CleanAsync(stoppingToken);
            }
            
            await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
        }
    }
}

public class ClosedGamesCleanerService(
    IGameRepository gameRepository,
    IUnitOfWork unitOfWork)
{
    public async Task CleanAsync(CancellationToken stoppingToken)
    {
        await gameRepository.DeleteClosedGamesAsync();
        await unitOfWork.SaveChangesAsync(stoppingToken);
    }
}