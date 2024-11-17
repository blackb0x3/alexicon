using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Alexicon.API.Domain.IoC;

public class DomainInstaller
{
    public static void Install(IServiceCollection services, IConfiguration config)
    {
        services.AddValidatorsFromAssembly(DomainAssembly);
    }

    private static readonly Assembly DomainAssembly = typeof(DomainInstaller).Assembly;
}