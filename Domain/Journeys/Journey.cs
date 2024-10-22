using Domain.Primitives;

namespace Domain.Journeys;

public sealed class Journey : AggregateRoot 
{
    public JourneyId Id { get; private set; }

    public String Origin { get; private set; } = string.Empty;

    public String Destination { get; private set; } = string.Empty;

    public double Price;

    public Journey(JourneyId id, string origin, string destination, double price)
    {
        Id = id;
        Origin = origin;
        Destination = destination;
        Price = price;
    }

    public Journey()
    {
    }
}