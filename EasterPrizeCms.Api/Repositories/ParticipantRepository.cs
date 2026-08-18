using EasterPrizeCms.Api.Data;
using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasterPrizeCms.Api.Repositories;

public class ParticipantRepository : IParticipantRepository
{
    private readonly EasterPrizeDbContext _context;

    public ParticipantRepository(EasterPrizeDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Participant>> GetAllAsync()
    {
        return await _context.Participants.ToListAsync();
    }

    public async Task<Participant?> GetByIdAsync(int id)
    {
        return await _context.Participants.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Participant participant)
    {
        await _context.Participants.AddAsync(participant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Participant participant)
    {
        _context.Participants.Update(participant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Participant participant)
    {
        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();
    }
}
