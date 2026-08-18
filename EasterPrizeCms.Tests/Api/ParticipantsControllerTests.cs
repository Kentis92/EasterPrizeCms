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

    public ParticipantsControllerTests(ParticipantsApiFactory factory)
    {
        _client = factory.CreateClient();
        _repository = factory.Repository;
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
}

public class ParticipantsApiFactory : WebApplicationFactory<Program>
{
    public FakeParticipantRepository Repository { get; } = new();

    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IParticipantRepository>();
            services.AddSingleton<IParticipantRepository>(Repository);
        });
    }
}

public class FakeParticipantRepository : IParticipantRepository
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
