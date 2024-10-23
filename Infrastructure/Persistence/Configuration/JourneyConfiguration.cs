using Domain.Flights;
using Domain.Journeys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class JourneyConfiguration : IEntityTypeConfiguration<Journey>
{
    public void Configure(EntityTypeBuilder<Journey> builder)
    {
        builder.HasKey(journey => journey.Id);
        
        builder.Property(journey => journey.Id).HasConversion(
            JourneyId => JourneyId.Value,
            value => new JourneyId(value)
        );

        builder.Property(journey => journey.Origin).HasMaxLength(3);

        builder.Property(journey => journey.Destination).HasMaxLength(3);

        builder.Ignore(journey => journey.Flights);

        builder.HasMany(journey => journey.Flights)
               .WithOne(flight => flight.Journey)
               .HasForeignKey(flight => flight.JourneyId)
               .OnDelete(DeleteBehavior.Cascade);
        
    }

}