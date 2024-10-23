namespace Domain.Flights;

public interface IFlightRepository
{
    void Add(Flight flight);
    Task<Flight?> GetFLightByOrigin(string origin);
    Task<Flight?> GetFlightByDestination(string destination);
    Task<List<Flight>> GetAll();
}