namespace TicTacToe.AuthService.Extensions;

public static class AddCorsExtension
{
    public static IServiceCollection AddCors(this IServiceCollection serviceCollection, string policy, string origin)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(policy,
                policyBuilder =>
                {
                    policyBuilder.WithOrigins(origin)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
        });

        return serviceCollection;
    }
}