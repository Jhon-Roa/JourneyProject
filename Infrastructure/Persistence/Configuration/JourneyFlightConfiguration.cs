using Domain.Journeys;
using Domain.JourneysFlights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class JourneyFlightConfiguration : IEntityTypeConfiguration<JourneyFlight>
{
    public void Configure(EntityTypeBuilder<JourneyFlight> builder)
    {
        builder.ToTable("journeys_flights");

        builder.HasKey(journeyFlight => new { journeyFlight.JourneyId, journeyFlight.FlightId });

        builder.Property(journeyFlight => journeyFlight.JourneyId)
               .IsRequired()
               .HasColumnName("journey_id");

        builder.Property(journeyFlight => journeyFlight.FlightId)
               .IsRequired()
               .HasColumnName("flight_id");

        builder.HasOne(journeyFlight => journeyFlight.Journey)
               .WithMany(journey => journey.JourneyFlights)
               .HasForeignKey(journeyFlight => journeyFlight.JourneyId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(journeyFlight => journeyFlight.Flight)
               .WithMany(flight => flight.JourneyFlights) 
               .HasForeignKey(journeyFlight => journeyFlight.FlightId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
