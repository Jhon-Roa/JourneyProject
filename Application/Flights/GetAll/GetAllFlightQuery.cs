using Application.Flights.Common;
using ErrorOr;
using MediatR;

namespace Application.Flights.GetAll;

public record GetAllFlightQuery() : IRequest<ErrorOr<IReadOnlyList<FlightResponse>>>;