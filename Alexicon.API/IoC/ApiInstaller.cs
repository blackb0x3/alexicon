using Alexicon.API.Domain.IoC;
using Alexicon.API.Domain.PrimaryPorts.CreateGame;
using Alexicon.API.Infrastructure.IoC;
using Mapster;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Alexicon.API.IoC;

public class ApiInstaller
{
    public static void Install(IServiceCollection services, IConfiguration config)
    {
        InfrastructureInstaller.Install(services, config);
        DomainInstaller.Install(services, config);

        services.AddMapster();
        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

        AddMapsterConfigurations();
    }

    private static void AddMapsterConfigurations()
    {
        TypeAdapterConfig<Models.Requests.CreateGameHttpRequest, CreateGameRequest>.NewConfig()
            .Map(dest => dest.Players, src => src.Players.Select(kvp => new NewPlayer(kvp.Key, kvp.Value.DisplayName, kvp.Value.StartingRack)))
            .PreserveReference(true);

        TypeAdapterConfig<Models.Requests.NewPlayerDto, NewPlayer>.NewConfig()
            .Map(dest => dest.DisplayName, src => src.DisplayName)
            .Map(dest => dest.StartingRack, src => src.StartingRack)
            .PreserveReference(true);
    }
}