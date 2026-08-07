using MiniHittegods.Domain.Entities;

namespace MiniHittegods.Application.Interfaces;

public interface IFoundItemRepository
{
    Task<FoundItem?> GetByIdAsync(Guid id);

    Task<List<FoundItem>> GetAllAsync();

    Task AddAsync(FoundItem item);

    Task DeleteAsync(Guid id);

    Task SaveChangesAsync();
}