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
        participant.Id = 1;

        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign(participant.Id);

        Assert.False(participant.CanDelete(new[] { prize }));
    }

    [Fact]
    public void Participant_with_collected_prizes_should_be_deletable()
    {
        var participant = new Participant("Ola Nordmann", 25, "Oslo");
        participant.Id = 1;

        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign(participant.Id);
        prize.Collect();

        Assert.True(participant.CanDelete(new[] { prize }));
    }

    [Fact]
    public void Participant_should_not_allow_empty_name()
    {
        Assert.Throws<ArgumentException>(() => new Participant(""));
    }

    [Fact]
    public void Participant_should_not_allow_name_shorter_than_2_characters()
    {
        Assert.Throws<ArgumentException>(() => new Participant("A"));
    }

    [Fact]
    public void Participant_should_not_allow_name_longer_than_80_characters()
    {
        var name = new string('A', 81);

        Assert.Throws<ArgumentException>(() => new Participant(name));
    }

    [Fact]
    public void Participant_should_not_allow_age_below_0()
    {
        Assert.Throws<ArgumentException>(() => new Participant("Ola Nordmann", -1));
    }

    [Fact]
    public void Participant_should_not_allow_age_above_120()
    {
        Assert.Throws<ArgumentException>(() => new Participant("Ola Nordmann", 121));
    }

    [Fact]
    public void Participant_should_not_allow_empty_city()
    {
        Assert.Throws<ArgumentException>(() => new Participant("Ola Nordmann", 25, ""));
    }

    [Fact]
    public void Participant_should_not_allow_city_shorter_than_2_characters()
    {
        Assert.Throws<ArgumentException>(() => new Participant("Ola Nordmann", 25, "A"));
    }

    [Fact]
    public void Participant_should_not_allow_city_longer_than_80_characters()
    {
        var city = new string('A', 81);

        Assert.Throws<ArgumentException>(() => new Participant("Ola Nordmann", 25, city));
    }
}
