using Domain.Flights;
using Domain.Primitives;

namespace Domain.Journeys;

public sealed class Journey : AggregateRoot 
{
    public JourneyId Id { get; private set; }
    public string Origin { get; private set; } = string.Empty;
    public string Destination { get; private set; } = string.Empty;
    public double Price { get; private set; }
    private readonly List<Flight> _flights = [];

    public IReadOnlyCollection<Flight> Flights => _flights.AsReadOnly();

    public Journey(JourneyId id, string origin, string destination, double price, List<Flight> flights)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        SetPrice(price);
        AddFlights(flights);
    }

    public Journey() { }

    public void SetPrice(double price)
    {
        if (price <= 0) throw new ArgumentException("Price must be greater than zero.", nameof(price));
        Price = price;
    }

    public void AddFlight(Flight flight)
    {
        if (flight == null) throw new ArgumentNullException(nameof(flight));
        _flights.Add(flight);
    }

    private void AddFlights(List<Flight> flights)
    {
        if (flights == null || !flights.Any()) throw new ArgumentException("At least one flight must be provided.", nameof(flights));
        _flights.AddRange(flights);
    }
}
