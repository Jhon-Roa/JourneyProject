using Domain.Journeys;
using Domain.ValueObjects;

namespace Domain.Flights;

public class Flight
{
    private Flight() {}

    public Flight(FlightId id, Transport transport, string origin, string destination, double price, JourneyId journeyId, Journey journey)
    {
        Id = id;
        Transport = transport;
        Origin = origin;
        Destination = destination;
        Price = price;
        JourneyId = journeyId;
        Journey = journey;
    }

    public FlightId Id { get; private set; }
    public Transport Transport { get; private set; }
    public string Origin { get; private set; } = string.Empty;
    public string Destination { get; private set; } = string.Empty;
    public double Price { get; private set; }

    public JourneyId JourneyId { get; private set; }
    public Journey Journey { get; private set; }

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