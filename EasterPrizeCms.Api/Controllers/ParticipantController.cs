using EasterPrizeCms.Application.DTOs;
using EasterPrizeCms.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EasterPrizeCms.Api.Controllers;

[ApiController]
[Route("api/participants")]
public class ParticipantsController : ControllerBase
{
    private readonly ParticipantService _service;

    public ParticipantsController(ParticipantService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParticipantResponse>>> GetAll()
    {
        var participants = await _service.GetAllAsync();

        var response = participants.Select(participant => new ParticipantResponse
        {
            Id = participant.Id,
            FullName = participant.FullName,
            Age = participant.Age,
            City = participant.City,
        });

        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ParticipantResponse>> GetById(int id)
    {
        var participant = await _service.GetByIdAsync(id);

        if (participant is null)
            return NotFound();

        return Ok(
            new ParticipantResponse
            {
                Id = participant.Id,
                FullName = participant.FullName,
                Age = participant.Age,
                City = participant.City,
            }
        );
    }

    [HttpGet("{id:int}/prizes")]
    public async Task<ActionResult<IEnumerable<PrizeResponse>>> GetPrizes(int id)
    {
        try
        {
            var prizes = await _service.GetPrizesAsync(id);

            var response = prizes.Select(prize => new PrizeResponse
            {
                Id = prize.Id,
                Name = prize.Name,
                Value = prize.Value,
                Status = prize.Status,
                ParticipantId = prize.ParticipantId,
            });

            return Ok(response);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<ActionResult<ParticipantResponse>> Create(CreateParticipantRequest request)
    {
        try
        {
            var participant = _service.Create(request.FullName, request.Age, request.City);
            await _service.AddAsync(participant);

            var response = new ParticipantResponse
            {
                Id = participant.Id,
                FullName = participant.FullName,
                Age = participant.Age,
                City = participant.City,
            };

            return CreatedAtAction(nameof(GetById), new { id = participant.Id }, response);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<ParticipantResponse>> Update(
        int id,
        UpdateParticipantRequest request
    )
    {
        try
        {
            var participant = await _service.UpdateAsync(
                id,
                request.FullName,
                request.Age,
                request.City
            );

            return Ok(
                new ParticipantResponse
                {
                    Id = participant.Id,
                    FullName = participant.FullName,
                    Age = participant.Age,
                    City = participant.City,
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
    }
}
