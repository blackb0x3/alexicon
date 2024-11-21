using Alexicon.API.Infrastructure.DataAccess;
using Alexicon.Helpers;
using EntityFramework.Exceptions.Sqlite;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alexicon.API.Infrastructure.IoC;

public class InfrastructureInstaller
{
    public static void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AlexiconContext>(options =>
        {
            options.UseSqlite(DatabasePathHelper.GetSqliteConnectionString())
                .UseExceptionProcessor();
        });

        AddMapsterConfigurations();
    }

    private static void AddMapsterConfigurations()
    {
        TypeAdapterConfig<Entities.GamePlayer, SecondaryPorts.DTOs.GamePlayer>.NewConfig()
            .Map(dest => dest.Player, src => src.Player)
            .Map(dest => dest.CurrentRack, src => src.CurrentRack)
            .PreserveReference(false);

        TypeAdapterConfig<Entities.GameMove, SecondaryPorts.DTOs.GameMove>.NewConfig()
            .Map(dest => dest.Player, src => src.Player)
            .Map(dest => dest.LettersUsed, src => src.LettersUsed)
            .Map(dest => dest.WordsCreated, src => src.WordsCreated)
            .PreserveReference(false);

        TypeAdapterConfig<Entities.Game, SecondaryPorts.DTOs.Game>.NewConfig()
            .Map(dest => dest.Players, src => src.Players.ToList().AsReadOnly())
            .Map(dest => dest.MovesPlayed, src => src.MovesPlayed.ToList().AsReadOnly())
            .PreserveReference(false);
    }
}