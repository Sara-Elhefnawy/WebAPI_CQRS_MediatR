using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Bookings.Queries.GetBookingById;

public record GetBookingByIdQueryRequest(int Id) : IRequest<Result<BookingDetailResponse>>;
