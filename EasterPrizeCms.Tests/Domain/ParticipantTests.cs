using EasterPrizeCms.Domain.Entities;

namespace EasterPrizeCms.Tests.Domain;

public class ParticipantTests
{
    [Fact]
    public void New_Participant_should_have_name()
    {
        var participant = new Participant("Ola Nordmann");

        Assert.Equal("Ola Nordmann", participant.Name);
    }

    [Fact]
    public void New_Participant_should_have_age()
    {
        var participant = new Participant("Ola Nordmann", 25);

        Assert.Equal(25, participant.Age);
    }

    [Fact]
    public void New_Participant_should_have_city()
    {
        var participant = new Participant("Ola Nordmann", 25, "Oslo");

        Assert.Equal("Oslo", participant.City);
    }

    [Fact]
    public void Participant_with_assigned_prize_should_not_be_deletable()
    {
        var participant = new Participant("Ola Nordmann", 25, "Oslo");
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();

        Assert.False(participant.CanDelete(new[] { prize }));
    }
    
    [Fact]
    public void Participant_without_assigned_prize_should_be_deletable()
    {
        var participant = new Participant("Ola Nordmann", 25, "Oslo");
        var prize = new Prize("Påskeegg XL", 250);

        Assert.True(participant.CanDelete(new[] { prize}));
    }
}