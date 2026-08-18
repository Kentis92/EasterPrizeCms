using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Application.Services;
using EasterPrizeCms.Domain.Entities;
using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Tests.Application;

public class PrizeServiceTests
{
    [Fact]
    public void Prize_service_can_be_created()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        Assert.NotNull(service);
    }

    [Fact]
    public void Create_prize_should_return_correct_data()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        var prize = service.Create("Påskeegg XL", 250);

        Assert.Equal("Påskeegg XL", prize.Name);
        Assert.Equal(250, prize.Value);
        Assert.Equal(PrizeStatus.InStock, prize.Status);
    }

    [Fact]
    public void Create_prize_should_reject_empty_name()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        Assert.Throws<ArgumentException>(() => service.Create("", 250));
    }

    [Fact]
    public void Create_prize_should_reject_name_shorter_than_2_characters()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        Assert.Throws<ArgumentException>(() => service.Create("A", 250));
    }

    [Fact]
    public void Create_prize_should_reject_name_longer_than_80_characters()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);
        var name = new string('A', 81);

        Assert.Throws<ArgumentException>(() => service.Create(name, 250));
    }

    [Fact]
    public void Create_prize_should_reject_negative_value()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        Assert.Throws<ArgumentException>(() => service.Create("Påskeegg XL", -1));
    }

    [Fact]
    public async Task Get_all_prizes_should_return_prizes()
    {
        var repository = new FakePrizeRepository();
        var prize = new Prize("Påskeegg XL", 250);
        await repository.AddAsync(prize);
        var service = new PrizeService(repository);

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("Påskeegg XL", result.First().Name);
    }

    [Fact]
    public async Task Get_prize_by_id_should_return_prize()
    {
        var repository = new FakePrizeRepository();
        var prize = new Prize("Påskeegg XL", 250);
        prize.Id = 1;
        await repository.AddAsync(prize);
        var service = new PrizeService(repository);

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Påskeegg XL", result.Name);
    }

    [Fact]
    public async Task Update_prize_should_update_data()
    {
        var repository = new FakePrizeRepository();
        var prize = new Prize("Påskeegg XL", 250);
        prize.Id = 1;
        await repository.AddAsync(prize);
        var service = new PrizeService(repository);

        await service.UpdateAsync(1, "Påskeegg XXL", 500);

        Assert.Equal("Påskeegg XXL", prize.Name);
        Assert.Equal(500, prize.Value);
    }

    [Fact]
    public async Task Delete_prize_should_remove_prize()
    {
        var repository = new FakePrizeRepository();
        var prize = new Prize("Påskeegg XL", 250);
        prize.Id = 1;
        await repository.AddAsync(prize);
        var service = new PrizeService(repository);

        await service.DeleteAsync(1);

        Assert.Empty(await repository.GetAllAsync());
    }

    [Fact]
    public async Task Assign_prize_should_assign_prize_to_participant()
    {
        var repository = new FakePrizeRepository();
        var prize = new Prize("Påskeegg XL", 250);
        prize.Id = 1;
        await repository.AddAsync(prize);
        var service = new PrizeService(repository);

        await service.AssignAsync(1, 5);

        Assert.Equal(PrizeStatus.Assigned, prize.Status);
        Assert.Equal(5, prize.ParticipantId);
    }

    [Fact]
    public async Task Collect_prize_should_mark_prize_as_collected()
    {
        var repository = new FakePrizeRepository();
        var prize = new Prize("Påskeegg XL", 250);
        prize.Id = 1;
        prize.Assign(5);
        await repository.AddAsync(prize);
        var service = new PrizeService(repository);

        await service.CollectAsync(1);

        Assert.Equal(PrizeStatus.Collected, prize.Status);
    }

    [Fact]
    public async Task Update_missing_prize_should_throw()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.UpdateAsync(999, "Påskeegg", 100));
    }

    [Fact]
    public async Task Delete_missing_prize_should_throw()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.DeleteAsync(999));
    }

    [Fact]
    public async Task Assign_missing_prize_should_throw()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.AssignAsync(999, 5));
    }

    [Fact]
    public async Task Collect_missing_prize_should_throw()
    {
        var repository = new FakePrizeRepository();
        var service = new PrizeService(repository);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.CollectAsync(999));
    }

    [Fact]
    public async Task Collected_prize_should_not_be_deletable()
    {
        var repository = new FakePrizeRepository();
        var prize = new Prize("Påskeegg XL", 250);
        prize.Id = 1;
        prize.Assign(5);
        prize.Collect();
        await repository.AddAsync(prize);
        var service = new PrizeService(repository);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.DeleteAsync(1));
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
