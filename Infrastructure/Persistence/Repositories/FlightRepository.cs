using Domain.Flights;
using Microsoft.EntityFrameworkCore;

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
        _context.Flights.Add(flight); 
    }

    public async Task<List<Flight>> GetAll()
    {
        return await _context.Flights.ToListAsync();
    }

    public async Task<Flight?> GetFlightByDestination(string destination)
    {
        return await _context.Flights
            .FirstOrDefaultAsync(f => f.Destination == destination); 
    }

    public async Task<Flight?> GetFLightByOrigin(string origin)
    {
        return await _context.Flights
            .FirstOrDefaultAsync(f => f.Origin == origin); 
    }
}
