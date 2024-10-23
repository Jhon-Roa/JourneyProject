using Domain.Flights;
using Domain.JourneysFlights;
using Domain.Primitives;

namespace Domain.Journeys;

public sealed class Journey : AggregateRoot 
{
    public Journey(JourneyId id, string origin, string destination, double price)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        SetPrice(price);    
    }

    public JourneyId Id { get; private set; }
    public string Origin { get; private set; } = string.Empty;
    public string Destination { get; private set; } = string.Empty;
    public double Price { get; private set; }
    
    public ICollection<JourneyFlight> JourneyFlights { get; private set; } = new List<JourneyFlight>();

    public void SetPrice(double price)
    {
        if (price <= 0) throw new ArgumentException("Price must be greater than zero.", nameof(price));
        Price = price;
    }
    public void AddFlight(FlightId flightId)
    {
        if (!JourneyFlights.Any(jf => jf.FlightId.Equals(flightId)))
        {
            JourneyFlights.Add(new JourneyFlight(Id, flightId));
        }
    }
}
