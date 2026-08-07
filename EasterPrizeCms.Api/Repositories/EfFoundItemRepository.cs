using Microsoft.EntityFrameworkCore;
using MiniHittegods.Api.Data;
using MiniHittegods.Application.Interfaces;
using MiniHittegods.Domain.Entities;

namespace MiniHittegods.Api.Repositories;

public class EfFoundItemRepository : IFoundItemRepository
{
    private readonly MiniHittegodsDbContext _context;

    public EfFoundItemRepository(MiniHittegodsDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FoundItem item)
    {
        await _context.FoundItems.AddAsync(item);
    }

    public async Task<FoundItem?> GetByIdAsync(Guid id)
    {
        return await _context.FoundItems
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<FoundItem>> GetAllAsync()
    {
        return await _context.FoundItems.ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await GetByIdAsync(id);

        if (item != null)
        {
            _context.FoundItems.Remove(item);
        }
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}