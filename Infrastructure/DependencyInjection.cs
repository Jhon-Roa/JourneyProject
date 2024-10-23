using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Domain.Primitives;
using Domain.Journeys;
using Infrastructure.Persistence.Repositories;
using Domain.Flights;
using Domain.JourneysFlights;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Database")));
        services.AddScoped<ApplicationDbContext>(scoped => scoped.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(scoped => scoped.GetRequiredService<IUnitOfWork>());

        services.AddScoped<IJourneyRepository,  JourneyRepository>();
        services.AddScoped<IFlightRepository, FlightRepository>();
        services.AddScoped<IJourneyFlightRepository, JourneyFlightsRepository>();

        return services;
    }
}