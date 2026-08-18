using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Application.Services;
using EasterPrizeCms.Domain.Entities;
using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Tests.Application;

public class ParticipantServiceTests
{
    [Fact]
    public void Participant_service_can_be_created()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);

        Assert.NotNull(service);
    }

    [Fact]
    public void Create_participant_should_return_correct_data()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);

        var participant = service.Create("Ola", 10, "Oslo");

        Assert.Equal("Ola", participant.Name);
        Assert.Equal(10, participant.Age);
        Assert.Equal("Oslo", participant.City);
    }

    [Fact]
    public void Create_participant_should_reject_negative_age()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);

        Assert.Throws<ArgumentException>(() => service.Create("Ola", -1, "Oslo"));
    }

    [Fact]
    public void Create_participant_should_reject_age_above_120()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);

        Assert.Throws<ArgumentException>(() => service.Create("Ola", 121, "Oslo"));
    }

    [Fact]
    public void Create_participant_should_reject_name_shorter_than_2_characters()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);

        Assert.Throws<ArgumentException>(() => service.Create("A", 10, "Oslo"));
    }

    [Fact]
    public void Create_participant_should_reject_name_longer_than_80_characters()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);
        var name = new string('A', 81);

        Assert.Throws<ArgumentException>(() => service.Create(name, 10, "Oslo"));
    }

    [Fact]
    public void Create_participant_should_reject_city_shorter_than_2_characters()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);

        Assert.Throws<ArgumentException>(() => service.Create("Ola", 10, "0"));
    }

    [Fact]
    public void Create_participant_should_reject_city_longer_than_80_characters()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var service = new ParticipantService(participantRepository, prizeRepository);
        var city = new string('A', 81);

        Assert.Throws<ArgumentException>(() => service.Create("Ola", 10, city));
    }

    [Fact]
    public async Task Get_all_participants_should_return_participants()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var participant = new Participant("Ola", 10, "Oslo");
        await participantRepository.AddAsync(participant);
        var service = new ParticipantService(participantRepository, prizeRepository);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("Ola", result.First().Name);
    }

    [Fact]
    public async Task Get_participant_by_id_should_return_participant()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var participant = new Participant("Ola", 10, "Oslo");
        participant.Id = 1;
        await participantRepository.AddAsync(participant);
        var service = new ParticipantService(participantRepository, prizeRepository);

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Ola", result.Name);
    }

    [Fact]
    public async Task Update_participant_should_update_data()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var participant = new Participant("Ola", 10, "Oslo");
        participant.Id = 1;
        await participantRepository.AddAsync(participant);
        var service = new ParticipantService(participantRepository, prizeRepository);

        await service.UpdateAsync(1, "Kari", 20, "Bergen");

        Assert.Equal("Kari", participant.Name);
        Assert.Equal(20, participant.Age);
        Assert.Equal("Bergen", participant.City);
    }

    [Fact]
    public async Task Delete_participant_should_remove_participant()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var participant = new Participant("Ola", 10, "Oslo");
        participant.Id = 1;
        await participantRepository.AddAsync(participant);
        var service = new ParticipantService(participantRepository, prizeRepository);

        await service.DeleteAsync(1);

        Assert.Empty(await participantRepository.GetAllAsync());
    }

    [Fact]
    public async Task Delete_participant_with_assigned_prize_should_throw()
    {
        var participantRepository = new FakeParticipantRepository();
        var prizeRepository = new FakePrizeRepository();
        var participant = new Participant("Ola", 10, "Oslo");
        participant.Id = 1;
        await participantRepository.AddAsync(participant);

        var prize = new Prize("Easter Egg", 100);
        prize.Id = 1;
        prize.Assign(participant.Id);
        await prizeRepository.AddAsync(prize);

        var service = new ParticipantService(participantRepository, prizeRepository);

        await Assert.ThrowsAsync<InvalidOperationException>(() => service.DeleteAsync(1));
    }

    private class FakeParticipantRepository : IParticipantRepository
    {
        private readonly List<Participant> _participants = [];

        public Task<IEnumerable<Participant>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Participant>>(_participants);
        }

        public Task<Participant?> GetByIdAsync(int id)
        {
            var participant = _participants.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(participant);
        }

        public Task AddAsync(Participant participant)
        {
            _participants.Add(participant);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Participant participant)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Participant participant)
        {
            _participants.Remove(participant);
            return Task.CompletedTask;
        }
    }

    private class FakePrizeRepository : IPrizeRepository
    {
        private readonly List<Prize> _prizes = [];

        public Task<IEnumerable<Prize>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Prize>>(_prizes);
        }

        public Task<Prize?> GetByIdAsync(int id)
        {
            var prize = _prizes.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(prize);
        }

        public Task AddAsync(Prize prize)
        {
            _prizes.Add(prize);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Prize prize)
        {
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Prize prize)
        {
            _prizes.Remove(prize);
            return Task.CompletedTask;
        }
    }
}
