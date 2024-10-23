using Domain.JourneysFlights;
using Domain.ValueObjects;

namespace Domain.Flights;

public class Flight
{
    public FlightId Id { get; private set; }
    public Transport Transport { get; private set; }
    public string Origin { get; private set; } = string.Empty;
    public string Destination { get; private set; } = string.Empty;
    public double Price { get; private set; }

    public ICollection<JourneyFlight> JourneyFlights { get; private set; } = new List<JourneyFlight>();

    private Flight() { }

    public Flight(FlightId id, Transport transport, string origin, string destination, double price)
    {
        Id = id;    
        Transport = transport;
        Origin = origin;
        Destination = destination;
        Price = price;
    }

    public void UpdatePrice(double newPrice)
    {
        if (newPrice < 0)
            throw new ArgumentException("El nuevo precio no puede ser negativo.");

        Price = newPrice;
    }

    public string GetFlightDetails()
    {
        return $"Vuelo ID: {Id}, Transporte: {Transport}, Origen: {Origin}, Destino: {Destination}, Precio: {Price:C}";
    }
}

