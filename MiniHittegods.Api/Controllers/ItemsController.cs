using Microsoft.AspNetCore.Mvc;
using MiniHittegods.Api.DTOs;
using MiniHittegods.Application.Services;
using MiniHittegods.Domain.Entities;

namespace MiniHittegods.Api.Controllers;

[ApiController]
[Route("api/items")]
public class ItemsController : ControllerBase
{
    private readonly FoundItemsService _service;

    public ItemsController(FoundItemsService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFoundItemRequest request)
    {
        var item = new FoundItem(
            request.Title,
            request.Description,
            request.Category,
            request.FoundLocation
        );

        await _service.CreateAsync(item);

        return CreatedAtAction(
            nameof(GetById),
            new { id = item.Id },
            item
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

[HttpPost("{id}/claim")]
public async Task<IActionResult> Claim(Guid id, ClaimItemRequest request)
{
    var item = await _service.GetByIdAsync(id);

    if (item == null)
    {
        return NotFound();
    }

    var result = await _service.ClaimAsync(id, request.ClaimedBy);

    return Ok(result);
}
}