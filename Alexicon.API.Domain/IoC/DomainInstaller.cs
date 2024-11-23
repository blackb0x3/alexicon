using System.Reflection;
using Alexicon.API.Domain.Services.Mappers;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alexicon.API.Domain.IoC;

public class DomainInstaller
{
    public static void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddValidatorsFromAssembly(DomainAssembly);
        
        AddMapsterConfigurations();
    }

    private static void AddMapsterConfigurations()
    {
        TypeAdapterConfig<SecondaryPorts.DTOs.Game, Representations.Games.GameRepresentation>.NewConfig()
            .MapWith(src => new GameDtoToGameRepresentationMapper().Map(src));
    }

    private static readonly Assembly DomainAssembly = typeof(DomainInstaller).Assembly;
}