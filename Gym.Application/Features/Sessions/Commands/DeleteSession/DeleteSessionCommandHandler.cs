using Gym.Application.UOW;
using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Commands.DeleteSession;

public sealed class DeleteSessionCommandHandler(IUnitOfWork uow) : IRequestHandler<DeleteSessionCommandRequest, Result>
{
    public async Task<Result> Handle(DeleteSessionCommandRequest request, CancellationToken ct)
    {
        var session = await uow.Sessions.GetByIdWithBookingsAsync(request.Id, ct);

        if (session is null)
            return Result.Fail("Session not found.", "SESSION_NOT_FOUND");

        // Business rule: only upcoming sessions can be deleted
        if (session.Status != "Upcoming")
            return Result.Fail(
                $"Cannot delete a {session.Status.ToLower()} session.",
                "SESSION_NOT_DELETABLE");

        uow.Sessions.Delete(session);
        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }
}
