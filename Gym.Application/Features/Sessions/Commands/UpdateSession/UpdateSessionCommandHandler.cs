using Gym.Application.UOW;
using Gym.Domain.Common;
using MediatR;

namespace Gym.Application.Features.Sessions.Commands.UpdateSession;

public sealed class UpdateSessionCommandHandler(IUnitOfWork uow) : IRequestHandler<UpdateSessionCommandRequest, Result>
{
    public async Task<Result> Handle(UpdateSessionCommandRequest request, CancellationToken ct)
    {
        var session = await uow.Sessions.GetByIdWithBookingsAsync(request.Id, ct);

        if (session is null)
            return Result.Fail("Session not found.", "SESSION_NOT_FOUND");

        if (string.IsNullOrWhiteSpace(request.TrainerName))
            return Result.Fail("Trainer name is required.", "TRAINER_REQUIRED");

        if (request.StartDate <= DateTime.Now)
            return Result.Fail("Start date must be in the future.", "START_DATE_INVALID");

        if (request.EndDate <= request.StartDate)
            return Result.Fail("End date must be after start date.", "END_DATE_INVALID");

        try
        {
            // Domain entity enforces its own rules
            session.Update(request.TrainerName, request.Description, request.StartDate, request.EndDate);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message, "INVALID_UPDATE");
        }

        uow.Sessions.Update(session);
        await uow.SaveChangesAsync(ct);

        return Result.Ok();
    }
}
