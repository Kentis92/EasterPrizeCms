using EasterPrizeCms.Domain.Entities;

namespace EasterPrizeCms.Application.Repositories;

public interface IParticipantRepository
{
    Task<IEnumerable<Participant>> GetAllAsync();
    Task<Participant?> GetByIdAsync(int id);
    Task AddAsync(Participant participant);
    Task UpdateAsync(Participant participant);
    Task DeleteAsync(Participant participant);
}
