using Microsoft.EntityFrameworkCore;
using TicTacToe.AuthService.DataAccess.Configurations;
using TicTacToe.AuthService.Entities;

namespace TicTacToe.AuthService.DataAccess;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}