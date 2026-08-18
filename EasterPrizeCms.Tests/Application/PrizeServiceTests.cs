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
