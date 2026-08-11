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
    [Fact]
    public void Create_participant_should_reject_negative_age()
    {
        var service = new ParticipantService();

        Assert.Throws<ArgumentException>(() =>
            service.Create("Ola", -1, "Oslo"));
    }
    [Fact]
    public void Create_participant_should_reject_age_above_120()
    {
        var service = new ParticipantService();

        Assert.Throws<ArgumentException>(() =>
            service.Create("Ola", 121, "Oslo"));
    }
    [Fact]
    public void Create_participant_should_reject_name_shorter_than_2_characters()
    {
        var service = new ParticipantService();

        Assert.Throws<ArgumentException>(() =>
            service.Create("A", 10, "Oslo"));
    }
    [Fact]
    public void Create_participant_should_reject_name_longer_than_80_characters()
    {
        var service = new ParticipantService();
        var name = new string('A', 81);

        Assert.Throws<ArgumentException>(() =>
            service.Create(name, 10, "Oslo"));
    }
    [Fact]
    public void Create_participant_should_reject_city_shorter_than_2_characters()
    {
        var service = new ParticipantService();
        
        Assert.Throws<ArgumentException>(() =>
            service.Create("Ola", 10, "0"));
    }
    [Fact]
    public void Create_participant_should_reject_city_longer_than_80_characters()
    {
        var service = new ParticipantService();
        var city = new string('A', 81);

        Assert.Throws<ArgumentException>(() =>
            service.Create("Ola", 10, city));
    }
}