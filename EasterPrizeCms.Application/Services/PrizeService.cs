using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Domain.Entities;

namespace EasterPrizeCms.Application.Services;

public class PrizeService
{
    private readonly IPrizeRepository _repository;

    public PrizeService(IPrizeRepository repository)
    {
        _repository = repository;
    }

    public Prize Create(string name, decimal value)
    {
        return new Prize(name, value);
    }

    public async Task AddAsync(Prize prize)
    {
        await _repository.AddAsync(prize);
    }

    public async Task<IEnumerable<Prize>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Prize?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Prize> UpdateAsync(int id, string name, decimal value)
    {
        var prize = await _repository.GetByIdAsync(id);

        if (prize is null)
            throw new KeyNotFoundException("Prize not found.");

        prize.Update(name, value);

        await _repository.UpdateAsync(prize);

        return prize;
    }

    public async Task DeleteAsync(int id)
    {
        var prize = await _repository.GetByIdAsync(id);

        if (prize is null)
            throw new KeyNotFoundException("Prize not found.");

        if (!prize.CanDelete())
            throw new InvalidOperationException("Collected prizes cannot be deleted.");

        await _repository.DeleteAsync(prize);
    }

    public async Task AssignAsync(int id, int participantId)
    {
        var prize = await _repository.GetByIdAsync(id);

        if (prize is null)
            throw new KeyNotFoundException("Prize not found.");

        prize.Assign(participantId);

        await _repository.UpdateAsync(prize);
    }

    public async Task CollectAsync(int id)
    {
        var prize = await _repository.GetByIdAsync(id);

        if (prize is null)
            throw new KeyNotFoundException("Prize not found.");

        prize.Collect();

        await _repository.UpdateAsync(prize);
    }
}
