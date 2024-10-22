namespace Domain.Flights;

public interface IFlightRepository
{
    Task<List<Flight>> GetAll();
    Task<Flight?> GetByIdAsync(FlightId id);
    Task<bool> ExistsAsync(FlightId id);
    void Add(Flight flight);
    void Update(Flight flight);
    void Delete(Flight flight);
}