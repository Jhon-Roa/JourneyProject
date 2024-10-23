using Domain.Flights;
using Domain.Journeys;
using Domain.JourneysFlights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class JourneyConfiguration : IEntityTypeConfiguration<Journey>
{
    public void Configure(EntityTypeBuilder<Journey> builder)
    {
        builder.ToTable("journeys");

        builder.HasKey(journey => journey.Id);
        
        builder.Property(journey => journey.Id).HasConversion(
            journeyId => journeyId.Value,
            value => new JourneyId(value)
        ).HasColumnName("journey_id");

        builder.Property(journey => journey.Origin)
               .HasMaxLength(3)
               .IsRequired()
               .HasColumnName("origin"); 

        builder.Property(journey => journey.Destination)
               .HasMaxLength(3)
               .IsRequired()
               .HasColumnName("destination"); 

        builder.HasMany(journey => journey.JourneyFlights)
               .WithOne(journeyFlight => journeyFlight.Journey)
               .HasForeignKey(journeyFlight => journeyFlight.JourneyId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
