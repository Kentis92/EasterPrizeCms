using MiniHittegods.Application.Interfaces;
using MiniHittegods.Domain.Entities;

namespace MiniHittegods.Application.Services;

public class FoundItemsService
{
    private readonly IFoundItemRepository _repository;

    public FoundItemsService(IFoundItemRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(FoundItem item)
    {
        await _repository.AddAsync(item);
        await _repository.SaveChangesAsync();
    }
}