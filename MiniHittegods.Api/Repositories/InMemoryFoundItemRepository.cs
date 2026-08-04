using MiniHittegods.Application.Interfaces;
using MiniHittegods.Domain.Entities;

namespace MiniHittegods.Api.Repositories;

public class InMemoryFoundItemRepository : IFoundItemRepository
{
    private readonly List<FoundItem> _items = new();

    public Task AddAsync(FoundItem item)
    {
        _items.Add(item);

        return Task.CompletedTask;
    }

    public Task<FoundItem?> GetByIdAsync(Guid id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        return Task.FromResult(item);
    }

    public Task<List<FoundItem>> GetAllAsync()
    {
        return Task.FromResult(_items.ToList());
    }

    public Task DeleteAsync(Guid id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);

        if (item != null)
        {
            _items.Remove(item);
        }

        return Task.CompletedTask;
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}