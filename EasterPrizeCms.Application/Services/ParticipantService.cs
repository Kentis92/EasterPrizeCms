using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Domain.Entities;

namespace EasterPrizeCms.Application.Services;

public class ParticipantService
{
    private readonly IParticipantRepository _repository;
    private readonly IPrizeRepository _prizeRepository;

    public ParticipantService(IParticipantRepository repository, IPrizeRepository prizeRepository)
    {
        _repository = repository;
        _prizeRepository = prizeRepository;
    }

    public Participant Create(string name, int age, string city)
    {
        if (age < 0 || age > 120)
            throw new ArgumentException("Age must be between 0 and 120.");

        if (name.Length < 2 || name.Length > 80)
            throw new ArgumentException("Name must be between 2 and 80 characters.");

        if (city.Length < 2 || city.Length > 80)
            throw new ArgumentException("City must be between 2 and 80 characters.");

        return new Participant(name, age, city);
    }

    public async Task AddAsync(Participant participant)
    {
        await _repository.AddAsync(participant);
    }

    public async Task<IEnumerable<Participant>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Participant?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Participant> UpdateAsync(int id, string name, int age, string city)
    {
        var participant = await _repository.GetByIdAsync(id);

        if (participant is null)
            throw new KeyNotFoundException("Participant not found.");

        if (age < 0 || age > 120)
            throw new ArgumentException("Age must be between 0 and 120.");

        if (name.Length < 2 || name.Length > 80)
            throw new ArgumentException("Name must be between 2 and 80 characters.");

        if (city.Length < 2 || city.Length > 80)
            throw new ArgumentException("City must be between 2 and 80 characters.");

        participant.Update(name, age, city);

        await _repository.UpdateAsync(participant);

        return participant;
    }

    public async Task DeleteAsync(int id)
    {
        var participant = await _repository.GetByIdAsync(id);

        if (participant is null)
            throw new KeyNotFoundException("Participant not found.");

        var prizes = await _prizeRepository.GetAllAsync();

        if (!participant.CanDelete(prizes))
            throw new InvalidOperationException(
                "Participant cannot be deleted while they have assigned prizes."
            );

        await _repository.DeleteAsync(participant);
    }
}
