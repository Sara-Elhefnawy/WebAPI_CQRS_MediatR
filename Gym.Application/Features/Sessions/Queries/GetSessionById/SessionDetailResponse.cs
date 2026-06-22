namespace Gym.Application.Features.Sessions.Queries.GetSessionById;

public record SessionDetailResponse(
    int Id,
    string CategoryName,
    string TrainerName,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    int Capacity,
    int AvailableSlots,
    string Status
);
