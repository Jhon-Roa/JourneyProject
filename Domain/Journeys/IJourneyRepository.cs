namespace Domain.Journeys;

public interface IJourneyRepository
{
    Task<List<Journey>> GetByOriginDestinationAsync(string origin, string destination);
    void Add(Journey journey);
    void Delete(Journey journey);
}