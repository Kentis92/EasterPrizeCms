using EasterPrizeCms.Application.DTOs;
using EasterPrizeCms.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasterPrizeCms.Api.Controllers;

[ApiController]
[Route("api/prizes")]
public class PrizesController : ControllerBase
{
    private readonly PrizeService _service;

    public PrizesController(PrizeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PrizeResponse>>> GetAll()
    {
        var prizes = await _service.GetAllAsync();

        var response = prizes.Select(prize => new PrizeResponse
        {
            Id = prize.Id,
            Name = prize.Name,
            Value = prize.Value,
            Status = prize.Status,
            ParticipantId = prize.ParticipantId,
            CreatedAtUtc = prize.CreatedAtUtc,
            UpdatedAtUtc = prize.UpdatedAtUtc,
        });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PrizeResponse>> GetById(int id)
    {
        var prize = await _service.GetByIdAsync(id);

        if (prize is null)
            return NotFound();

        return Ok(
            new PrizeResponse
            {
                Id = prize.Id,
                Name = prize.Name,
                Value = prize.Value,
                Status = prize.Status,
                ParticipantId = prize.ParticipantId,
                CreatedAtUtc = prize.CreatedAtUtc,
                UpdatedAtUtc = prize.UpdatedAtUtc,
            }
        );
    }

    [HttpPost]
    public async Task<ActionResult<PrizeResponse>> Create(CreatePrizeRequest request)
    {
        try
        {
            var prize = _service.Create(request.Name, request.Value);
            await _service.AddAsync(prize);

            var response = new PrizeResponse
            {
                Id = prize.Id,
                Name = prize.Name,
                Value = prize.Value,
                Status = prize.Status,
                ParticipantId = prize.ParticipantId,
                CreatedAtUtc = prize.CreatedAtUtc,
                UpdatedAtUtc = prize.UpdatedAtUtc,
            };

            return CreatedAtAction(nameof(GetById), new { id = prize.Id }, response);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PrizeResponse>> Update(int id, UpdatePrizeRequest request)
    {
        try
        {
            var prize = await _service.UpdateAsync(id, request.Name, request.Value);

            return Ok(
                new PrizeResponse
                {
                    Id = prize.Id,
                    Name = prize.Name,
                    Value = prize.Value,
                    Status = prize.Status,
                    ParticipantId = prize.ParticipantId,
                    CreatedAtUtc = prize.CreatedAtUtc,
                    UpdatedAtUtc = prize.UpdatedAtUtc,
                }
            );
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPost("{id:int}/assign")]
    public async Task<IActionResult> Assign(int id, AssignPrizeRequest request)
    {
        try
        {
            await _service.AssignAsync(id, request.ParticipantId);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPost("{id:int}/collect")]
    public async Task<IActionResult> Collect(int id)
    {
        try
        {
            await _service.CollectAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
