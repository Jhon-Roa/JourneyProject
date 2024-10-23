using Domain.Flights;
using Domain.Journeys;
using Domain.JourneysFlights;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Journey> Journeys { get; set; }
    DbSet<Flight> Flights { get; set; }
    DbSet<JourneyFlight> JourneyFlights { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}