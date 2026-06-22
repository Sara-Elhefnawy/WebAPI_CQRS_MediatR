using Gym.API.ViewModels.Sessions;
using Gym.Application.Features.Sessions.Commands.CreateSession;
using Gym.Application.Features.Sessions.Commands.DeleteSession;
using Gym.Application.Features.Sessions.Commands.UpdateSession;
using Gym.Application.Features.Sessions.Queries.GetSessionById;
using Gym.Application.Features.Sessions.Queries.GetSessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gym.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SessionsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSessions()
    {
        var query = new GetSessionsQueryRequest();
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        var viewModels = result.Value.Select(response => new SessionViewModel
        {
            Id = response.Id,
            CategoryName = response.CategoryName,
            TrainerName = response.TrainerName,
            Description = response.Description,
            StartDate = response.StartDate,
            EndDate = response.EndDate,
            Capacity = response.Capacity,
            AvailableSlots = response.AvailableSlots,
            Status = response.Status
        }).ToList();

        return Ok(viewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSessionById(int id)
    {
        var query = new GetSessionByIdQueryRequest(id);
        var result = await mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(result.Error);

        var viewModel = new SessionViewModel
        {
            Id = result.Value.Id,
            CategoryName = result.Value.CategoryName,
            TrainerName = result.Value.TrainerName,
            Description = result.Value.Description,
            StartDate = result.Value.StartDate,
            EndDate = result.Value.EndDate,
            Capacity = result.Value.Capacity,
            AvailableSlots = result.Value.AvailableSlots,
            Status = result.Value.Status
        };

        return Ok(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSession([FromBody] CreateSessionViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateSessionCommandRequest(
            viewModel.CategoryName,
            viewModel.TrainerName,
            viewModel.Description,
            viewModel.StartDate,
            viewModel.EndDate,
            viewModel.Capacity
        );

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return CreatedAtAction(nameof(GetSessionById), new { id = result.Value }, result.Value);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSession(int id, [FromBody] UpdateSessionViewModel viewModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new UpdateSessionCommandRequest(
            id,
            viewModel.TrainerName,
            viewModel.Description,
            viewModel.StartDate,
            viewModel.EndDate
        );

        var result = await mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSession(int id)
    {
        var command = new DeleteSessionCommandRequest(id);
        var result = await mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.Error);

        return NoContent();
    }
}
