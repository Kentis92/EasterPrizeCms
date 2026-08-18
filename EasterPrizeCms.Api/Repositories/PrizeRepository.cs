using EasterPrizeCms.Api.Data;
using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasterPrizeCms.Api.Repositories;

public class PrizeRepository : IPrizeRepository
{
    private readonly EasterPrizeDbContext _context;

    public PrizeRepository(EasterPrizeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prize>> GetAllAsync()
    {
        return await _context.Prizes.ToListAsync();
    }

    public async Task<Prize?> GetByIdAsync(int id)
    {
        return await _context.Prizes.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Prize prize)
    {
        await _context.Prizes.AddAsync(prize);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Prize prize)
    {
        _context.Prizes.Update(prize);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Prize prize)
    {
        _context.Prizes.Remove(prize);
        await _context.SaveChangesAsync();
    }
}
