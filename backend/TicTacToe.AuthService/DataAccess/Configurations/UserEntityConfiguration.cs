using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.AuthService.Entities;

namespace TicTacToe.AuthService.DataAccess.Configurations;

public class UserEntityConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u  => u.Id);
        builder.HasIndex(u => u.Login).IsUnique();
    }
}
