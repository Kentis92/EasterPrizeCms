using System.Net;
using System.Net.Http.Json;
using EasterPrizeCms.Application.DTOs;
using EasterPrizeCms.Application.Repositories;
using EasterPrizeCms.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace EasterPrizeCms.Tests.Api;

public class ParticipantsControllerTests : IClassFixture<ParticipantsApiFactory>
{
    private readonly HttpClient _client;
    private readonly FakeParticipantRepository _repository;
    private readonly FakePrizeRepository _prizeRepository;

    public ParticipantsControllerTests(ParticipantsApiFactory factory)
    {
        _client = factory.CreateClient();
        _repository = factory.Repository;
        _prizeRepository = factory.PrizeRepository;
        _repository.Clear();
        _prizeRepository.Clear();
    }

    [Fact]
    public async Task Get_participants_should_return_200_ok()
    {
        var response = await _client.GetAsync("/api/participants");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_missing_participant_should_return_404_not_found()
    {
        var response = await _client.GetAsync("/api/participants/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Post_participant_should_return_201_created()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "Ola Nordmann",
            Age = 10,
            City = "Oslo",
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Post_participant_should_return_location_header()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "Ola Nordmann",
            Age = 10,
            City = "Oslo",
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
    }

    [Fact]
    public async Task Post_invalid_participant_should_return_400_bad_request()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "A",
            Age = 10,
            City = "Oslo",
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_participant_with_invalid_age_should_return_400_bad_request()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "Ola Nordmann",
            Age = 121,
            City = "Oslo",
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_participant_with_negative_age_should_return_400_bad_request()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "Ola Nordmann",
            Age = -1,
            City = "Oslo",
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_participant_with_empty_city_should_return_400_bad_request()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "Ola Nordmann",
            Age = 10,
            City = "",
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_participant_with_city_shorter_than_2_characters_should_return_400_bad_request()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "Ola Nordmann",
            Age = 10,
            City = "O",
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Post_participant_with_city_longer_than_80_characters_should_return_400_bad_request()
    {
        var request = new CreateParticipantRequest
        {
            FullName = "Ola Nordmann",
            Age = 10,
            City = new string('A', 81),
        };

        var response = await _client.PostAsJsonAsync("/api/participants", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task Put_participant_should_return_200_ok()
    {
        var participant = new Participant("Ola Nordmann", 10, "Oslo") { Id = 1 };

        await _repository.AddAsync(participant);

        var request = new UpdateParticipantRequest
        {
            FullName = "Kari Nordmann",
            Age = 12,
            City = "Bergen",
        };

        var response = await _client.PutAsJsonAsync("/api/participants/1", request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Put_missing_participant_should_return_404_not_found()
    {
        var request = new UpdateParticipantRequest
        {
            FullName = "Kari Nordmann",
            Age = 12,
            City = "Bergen",
        };

        var response = await _client.PutAsJsonAsync("/api/participants/999", request);

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_participant_should_return_204_no_content()
    {
        var participant = new Participant("Ola Nordmann", 10, "Oslo") { Id = 1 };

        await _repository.AddAsync(participant);

        var response = await _client.DeleteAsync("/api/participants/1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_missing_participant_should_return_404_not_found()
    {
        var response = await _client.DeleteAsync("/api/participants/999");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Delete_participant_with_assigned_prize_should_return_409_conflict()
    {
        var participant = new Participant("Ola Nordmann", 10, "Oslo") { Id = 1 };
        var prize = new Prize("Påskeegg XL", 250) { Id = 1 };

        prize.Assign(participant.Id);

        await _repository.AddAsync(participant);
        await _prizeRepository.AddAsync(prize);

        var response = await _client.DeleteAsync("/api/participants/1");

        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task Get_participant_prizes_should_return_200_ok()
    {
        var participant = new Participant("Ola Nordmann", 10, "Oslo") { Id = 1 };

        await _repository.AddAsync(participant);

        var response = await _client.GetAsync("/api/participants/1/prizes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Get_participant_prizes_should_return_assigned_prizes()
    {
        var participant = new Participant("Ola Nordmann", 10, "Oslo") { Id = 1 };
        var prize = new Prize("Påskeegg XL", 250) { Id = 1 };

        prize.Assign(participant.Id);

        await _repository.AddAsync(participant);
        await _prizeRepository.AddAsync(prize);

        var response = await _client.GetAsync("/api/participants/1/prizes");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<List<PrizeResponse>>();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Påskeegg XL", result[0].Name);
        Assert.Equal(250, result[0].Value);
        Assert.Equal(participant.Id, result[0].ParticipantId);
    }
}

public class ParticipantsApiFactory : WebApplicationFactory<Program>
{
    public FakeParticipantRepository Repository { get; } = new();
    public FakePrizeRepository PrizeRepository { get; } = new();

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IParticipantRepository>();
            services.RemoveAll<IPrizeRepository>();

            services.AddSingleton<IParticipantRepository>(Repository);
            services.AddSingleton<IPrizeRepository>(PrizeRepository);
        });
    }
}

public class FakeParticipantRepository : IParticipantRepository
{
    private readonly List<Participant> _participants = [];

    public void Clear()
    {
        _participants.Clear();
    }

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
        if (participant.Id == 0)
            participant.Id = _participants.Count + 1;

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
