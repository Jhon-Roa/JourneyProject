using Application.Data;
using Domain.Flights;
using Domain.Journeys;
using Domain.JourneysFlights;
using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher? _publisher;
    
    public ApplicationDbContext(DbContextOptions options, IPublisher? publisher = null): base(options)
    {
        _publisher = publisher;
    }
    
    public DbSet<Journey> Journeys { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<JourneyFlight> JourneyFlights { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Flight>()
            .OwnsOne(f => f.Transport);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        
        if (_publisher != null)
        {
            var domainEvents = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());
                
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
        
        return result;
    }
}