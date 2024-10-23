using Application.Flights.Create;
using Application.Flights.GetAll;
using Application.Flights.Services;
using Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;

namespace WebApi.Controller;

    [ApiController]
    [Route("flights")]
    public class FlightController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IFlightApiService _flightApiService;

        public FlightController(ISender mediator, IFlightApiService flightApiService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _flightApiService = flightApiService ?? throw new ArgumentNullException(nameof(flightApiService));
        }

        [HttpGet("sync-flights")]
        public async Task<IActionResult> SyncFlights(CancellationToken cancellationToken)
        {
            var existingFlightsResult = await _mediator.Send(new GetAllFlightQuery(), cancellationToken);

            if (existingFlightsResult.IsError)
            {
                return BadRequest(existingFlightsResult.Errors);
            }

            var existingFlights = existingFlightsResult.Value;

            var apiFlights = await _flightApiService.GetFlightsAsync(cancellationToken);

            var newFlights = apiFlights
                .Where(apiFlight => !existingFlights.Any(existingFlight =>
                    existingFlight.Origin == apiFlight.DepartureStation &&
                    existingFlight.Destination == apiFlight.ArrivalStation &&
                    existingFlight.Price == (decimal)apiFlight.Price &&
                    existingFlight.Transport.FlightCarrier == apiFlight.FlightCarrier &&
                    existingFlight.Transport.FlightNumber == apiFlight.FlightNumber))
                .ToList();

            foreach (var flight in newFlights)
            {
                var transport = Transport.Create(flight.FlightCarrier, flight.FlightNumber);
                if (transport is not null)
                {
                    var command = new CreateFlightCommand(
                        flight.DepartureStation,
                        flight.ArrivalStation,
                        flight.Price,
                        transport.FlightCarrier,
                        transport.FlightNumber
                    );

                    var createResult = await _mediator.Send(command, cancellationToken);

                    if (createResult.IsError)
                    {
                        return BadRequest(createResult.Errors);
                    }
                }
            }

            return Ok(newFlights.Count);
        }
    }