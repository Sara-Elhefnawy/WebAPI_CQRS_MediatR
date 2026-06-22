using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Queries.GetSessionById;

public record GetSessionByIdQueryRequest(int Id) : IRequest<Result<SessionDetailResponse>>;
