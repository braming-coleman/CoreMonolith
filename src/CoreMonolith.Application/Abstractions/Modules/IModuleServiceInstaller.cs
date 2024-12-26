using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreMonolith.Application.Abstractions.Modules;

public interface IModuleServicesInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration config);

    void InstallDatabase(IHostApplicationBuilder builder);

    void ApplyMigrations(IApplicationBuilder app);
}
