using Domain.Journeys;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Journey> Journeys { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}