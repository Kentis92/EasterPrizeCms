using System.Net;
using System.Net.Http.Json;
using EasterPrizeCms.Application.DTOs;
using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasterPrizeCms.Tests.Api;

public class PrizesControllerTests : IClassFixture<PrizesApiFactory>
{
    private readonly HttpClient _client;
    private readonly FakePrizeRepository _repository;

    public PrizesControllerTests(PrizesApiFactory factory)
    {
        _client = factory.CreateClient();
        _repository = factory.Repository;
        _repository.Clear();
    }

    [Fact]
    public async Task Get_prizes_should_return_200_ok()
    {
        var response = await _client.GetAsync("/api/prizes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_missing_prize_should_return_404_not_found()
    {
        var response = await _client.GetAsync("/api/prizes/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_prize_should_return_201_created()
    {
        var request = new CreatePrizeRequest { Name = "Nintendo Switch", Value = 3000 };

        var response = await _client.PostAsJsonAsync("/api/prizes", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Post_invalid_prize_should_return_400_bad_request()
    {
        var request = new CreatePrizeRequest { Name = "", Value = 3000 };

        var response = await _client.PostAsJsonAsync("/api/prizes", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_prize_should_return_200_ok()
    {
        var prize = new Prize("Nintendo Switch", 3000) { Id = 1 };

        await _repository.AddAsync(prize);

        var request = new UpdatePrizeRequest { Name = "PlayStation 5", Value = 5000 };

        var response = await _client.PutAsJsonAsync("/api/prizes/1", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Put_missing_prize_should_return_404_not_found()
    {
        var request = new UpdatePrizeRequest { Name = "PlayStation 5", Value = 5000 };

        var response = await _client.PutAsJsonAsync("/api/prizes/999", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_prize_should_return_204_no_content()
    {
        var prize = new Prize("Nintendo Switch", 3000) { Id = 1 };

        await _repository.AddAsync(prize);

        var response = await _client.DeleteAsync("/api/prizes/1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_missing_prize_should_return_404_not_found()
    {
        var response = await _client.DeleteAsync("/api/prizes/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Assign_prize_should_return_204_no_content()
    {
        var prize = new Prize("Nintendo Switch", 3000) { Id = 1 };

        await _repository.AddAsync(prize);

        var request = new AssignPrizeRequest { ParticipantId = 1 };

        var response = await _client.PostAsJsonAsync("/api/prizes/1/assign", request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Collect_prize_should_return_204_no_content()
    {
        var prize = new Prize("Nintendo Switch", 3000) { Id = 1 };
        prize.Assign(1);

        await _repository.AddAsync(prize);

        var response = await _client.PostAsync("/api/prizes/1/collect", null);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Assign_already_assigned_prize_should_return_409_conflict()
    {
        var prize = new Prize("Nintendo Switch", 3000) { Id = 1 };
        prize.Assign(1);

        await _repository.AddAsync(prize);

        var request = new AssignPrizeRequest { ParticipantId = 2 };

        var response = await _client.PostAsJsonAsync("/api/prizes/1/assign", request);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Collect_in_stock_prize_should_return_409_conflict()
    {
        var prize = new Prize("Nintendo Switch", 3000) { Id = 1 };

        await _repository.AddAsync(prize);

        var response = await _client.PostAsync("/api/prizes/1/collect", null);

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Delete_collected_prize_should_return_409_conflict()
    {
        var prize = new Prize("Nintendo Switch", 3000) { Id = 1 };
        prize.Assign(1);
        prize.Collect();

        await _repository.AddAsync(prize);

        var response = await _client.DeleteAsync("/api/prizes/1");

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }
}

public class PrizesApiFactory : WebApplicationFactory<Program>
{
    public FakePrizeRepository Repository { get; } = new();

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IPrizeRepository>();
            services.AddSingleton<IPrizeRepository>(Repository);
        });
    }
}

public class FakePrizeRepository : IPrizeRepository
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
        if (prize.Id == 0)
            prize.Id = _prizes.Count + 1;

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

    public void Clear()
    {
        _prizes.Clear();
    }
}
