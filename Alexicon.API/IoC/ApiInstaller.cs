using Alexicon.API.Domain.IoC;
using Alexicon.API.Infrastructure.IoC;

namespace Alexicon.API.IoC;

public class ApiInstaller
{
    public static void Install(IServiceCollection services, IConfiguration config)
    {
        InfrastructureInstaller.Install(services, config);
        DomainInstaller.Install(services, config);

        services.AddMediator();
    }
}