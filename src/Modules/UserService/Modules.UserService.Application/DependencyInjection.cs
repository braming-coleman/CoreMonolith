using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.UserService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddUserServiceApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);

        return services;
    }
}
