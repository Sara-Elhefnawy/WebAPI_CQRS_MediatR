using Gym.API.ViewModels.Bookings;
using Gym.Application.Features.Bookings.Commands.CancelBooking;
using Gym.Application.Features.Bookings.Commands.CreateBooking;
using Gym.Application.Features.Bookings.Queries.GetBookingById;
using Gym.Application.Features.Sessions.Queries.GetSessionBookings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookingById(int id)
    {
        var query = new GetBookingByIdQueryRequest(id);
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        var viewModel = new BookingViewModel
        {
            Id = result.Value.Id,
            SessionId = result.Value.SessionId,
            MemberId = result.Value.MemberId,
            BookingDate = result.Value.BookingDate,
            IsAttended = result.Value.IsAttended
        };

        return Ok(viewModel);
    }

    [HttpGet("session/{sessionId}")]
    public async Task<IActionResult> GetSessionBookings(int sessionId)
    {
        var query = new GetSessionBookingsQueryRequest(sessionId);
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        var viewModels = result.Value.Select(response => new BookingViewModel
        {
            Id = response.Id,
            SessionId = sessionId,
            MemberId = response.MemberId,
            BookingDate = response.BookingDate,
            IsAttended = response.IsAttended
        }).ToList();

        return Ok(viewModels);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] CreateBookingViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateBookingCommandRequest(
            viewModel.SessionId,
            viewModel.MemberId
        );

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetBookingById), new { id = result.Value }, result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelBooking(int id)
    {
        var command = new CancelBookingCommandRequest(id);
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }
}
