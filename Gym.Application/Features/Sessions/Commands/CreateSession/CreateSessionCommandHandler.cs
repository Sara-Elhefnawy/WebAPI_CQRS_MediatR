using Gym.Application.UOW;
using Gym.Domain.Common;
using Gym.Domain.Entities;
using MediatR;

namespace Gym.Application.Features.Sessions.Commands.CreateSession;

public sealed class CreateSessionCommandHandler(IUnitOfWork uow)
        : IRequestHandler<CreateSessionCommandRequest, Result<int>>
{
    public async Task<Result<int>> Handle(CreateSessionCommandRequest request, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(request.CategoryName))
            return Result.Fail<int>("Category name is required.", "CATEGORY_REQUIRED");

        if (string.IsNullOrWhiteSpace(request.TrainerName))
            return Result.Fail<int>("Trainer name is required.", "TRAINER_REQUIRED");

        if (request.StartDate <= DateTime.Now)
            return Result.Fail<int>("Start date must be in the future.", "START_DATE_INVALID");

        if (request.EndDate <= request.StartDate)
            return Result.Fail<int>("End date must be after start date.", "END_DATE_INVALID");

        if (request.Capacity < 1 || request.Capacity > 25)
            return Result.Fail<int>("Capacity must be between 1 and 25.", "CAPACITY_INVALID");

        // Create session using domain entity
        Session session;
        try
        {
            session = Session.Create(
                categoryName: request.CategoryName,
                trainerName: request.TrainerName,
                description: request.Description,
                startDate: request.StartDate,
                endDate: request.EndDate,
                capacity: request.Capacity);
        }
        catch (ArgumentException ex)
        {
            return Result.Fail<int>(ex.Message, "INVALID_SESSION");
        }

        await uow.Sessions.AddAsync(session, ct);
        await uow.SaveChangesAsync(ct);

        return Result.Ok(session.Id);
    }
}
