
using KanBanApplication.InfraStructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KanBanApplication.InfraStructure;

public static  class DependencyConfig {
  

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Default");

        services.AddDbContext<KanbanContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}