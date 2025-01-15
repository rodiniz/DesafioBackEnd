using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using KanBanApplication.InfraStructure.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace KanBanApplication.InfraStructure;

[ExcludeFromCodeCoverage]
public static class ServicesExtension
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<KanbanCardValidator>();
    }
}