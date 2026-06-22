using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Commands.DeleteSession;

public record DeleteSessionCommandRequest(int Id) : IRequest<Result>;
