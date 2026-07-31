using MiniHittegods.Application.Interfaces;
using MiniHittegods.Domain.Entities;

namespace MiniHittegods.Tests.Fakes;

public class FakeFoundItemRepository : IFoundItemRepository
{
    private readonly List<FoundItem> _items = new();

    public Task AddAsync(FoundItem item)
    {
        _items.Add(item);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _items.RemoveAll(x => x.Id == id);
        return Task.CompletedTask;
    }

    public Task<List<FoundItem>> GetAllAsync()
    {
        return Task.FromResult(_items);
    }

    public Task<FoundItem?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
    }

    public Task SaveChangesAsync()
    {
        return Task.CompletedTask;
    }
}