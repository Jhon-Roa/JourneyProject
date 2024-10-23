using Application.Flights.Services.Models;

namespace Application.Flights.Services;

public interface IFlightApiService
{
    Task<List<FlightDto>> GetFlightsAsync(CancellationToken cancellationToken);
}
