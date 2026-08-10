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
}