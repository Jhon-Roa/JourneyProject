using Domain.Flights;
using Domain.JourneysFlights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration;

public class FlightConfiguration : IEntityTypeConfiguration<Flight>
{
    public void Configure(EntityTypeBuilder<Flight> builder)
    {
        builder.ToTable("flights");

        builder.HasKey(flight => flight.Id);
        
        builder.Property(flight => flight.Id).HasConversion(
            flightId => flightId.Value,
            value => new FlightId(value)
        ).HasColumnName("flight_id");

        builder.Property(flight => flight.Origin)
               .IsRequired()
               .HasMaxLength(3)
               .HasColumnName("origin"); 

        builder.Property(flight => flight.Destination)
               .IsRequired()
               .HasMaxLength(3)
               .HasColumnName("destination"); 

        builder.Property(flight => flight.Price)
               .IsRequired()
               .HasColumnName("price");

        builder.OwnsOne(flight => flight.Transport, transport =>
        {
            transport.Property(t => t.FlightCarrier)
                     .IsRequired()
                     .HasMaxLength(2)
                     .HasColumnName("flight_carrier");

            transport.Property(t => t.FlightNumber)
                     .IsRequired()
                     .HasMaxLength(4)
                     .HasColumnName("flight_number");
        });

        builder.HasMany(flight => flight.JourneyFlights)
               .WithOne(journeyFlight => journeyFlight.Flight)
               .HasForeignKey(journeyFlight => journeyFlight.FlightId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
