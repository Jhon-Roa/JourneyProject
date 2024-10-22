namespace Domain.Journeys;

public interface IJourneyRepository
{
    Task<List<Journey>> GetAll();
    Task<Journey?> GetByIdAsync(JourneyId id);
    Task<bool> ExistsAsync(JourneyId id);
    void Add(Journey journey);
    void Update(Journey journey);
    void Delete(Journey journey);
}