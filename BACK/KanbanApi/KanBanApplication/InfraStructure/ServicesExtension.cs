using FluentValidation;
using KanBanApplication.InfraStructure.Validators;
using Microsoft.Extensions.DependencyInjection;

namespace KanBanApplication.InfraStructure;

public static class ServicesExtension
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining<KanbanCardValidator>();
    }
}