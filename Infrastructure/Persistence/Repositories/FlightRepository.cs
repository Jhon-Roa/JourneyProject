using Domain.Flights;

namespace Infrastructure.Persistence.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly ApplicationDbContext _context;

    public FlightRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Add(Flight flight)
    {
        throw new NotImplementedException();
    }

    public Task<Flight?> GetFlightByDestination(string destination)
    {
        throw new NotImplementedException();
    }

    public Task<Flight?> GetFLightByOrigin(string origin)
    {
        throw new NotImplementedException();
    }
}