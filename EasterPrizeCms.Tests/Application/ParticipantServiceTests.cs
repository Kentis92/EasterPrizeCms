using EasterPrizeCms.Application.Services;

namespace EasterPrizeCms.Tests.Application;

public class ParticipantServiceTests
{
    [Fact]
    public void Participant_service_can_be_created()
    {
        var service = new ParticipantService();

        Assert.NotNull(service);
    }

    [Fact]
    public void Create_participant_should_return_correct_data()
    {
        var service = new ParticipantService();

        var participant = service.Create("Ola", 10, "Oslo");

        Assert.Equal("Ola", participant.Name);
        Assert.Equal(10, participant.Age);
        Assert.Equal("Oslo", participant.City);
    }
}