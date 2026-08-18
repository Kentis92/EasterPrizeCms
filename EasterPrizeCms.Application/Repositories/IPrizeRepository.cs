using EasterPrizeCms.Domain.Entities;

namespace EasterPrizeCms.Application.Repositories;

public interface IPrizeRepository
{
    Task<IEnumerable<Prize>> GetAllAsync();
    Task<Prize?> GetByIdAsync(int id);
    Task AddAsync(Prize prize);
    Task UpdateAsync(Prize prize);
    Task DeleteAsync(Prize prize);
}
