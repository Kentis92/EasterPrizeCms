using EasterPrizeCms.Domain.Entities;
using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Tests.Domain;

public class PrizeTests
{
    [Fact]
    public void New_prize_should_have_status_in_stock()
    {
        var prize = new Prize();

        Assert.Equal(PrizeStatus.InStock, prize.Status);
    }

    [Fact]
    public void New_prize_should_have_name()
    {
        var prize = new Prize("Påskeegg XL");

        Assert.Equal("Påskeegg XL", prize.Name);
    }

    [Fact]
    public void Prize_should_not_allow_empty_name()
    {
        Assert.Throws<ArgumentException>(() => new Prize(""));
    }

    [Fact]
    public void New_prize_should_have_value()
    {
        var prize = new Prize("Påskeegg XL", 250);

        Assert.Equal(250, prize.Value);
    }

    [Fact]
    public void Prize_should_not_allow_negative_value()
    {
        Assert.Throws<ArgumentException>(() => new Prize("Påskeegg XL", -1));
    }

    [Fact]
    public void Prize_should_be_assignable_when_in_stock()
    {
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();

        Assert.Equal(PrizeStatus.Assigned, prize.Status);
    }

    [Fact]
    public void Assigned_prize_should_not_be_assignable_again()
    {
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();

        Assert.Throws<InvalidOperationException>(() => prize.Assign());
    }

    [Fact]
    public void Collected_prize_should_not_be_assignable()
    {
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();
        prize.Collect();

        Assert.Throws<InvalidOperationException>(() => prize.Assign());
    }

    [Fact]
    public void Assigned_prize_should_be_collectable()
    {
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();
        prize.Collect();

        Assert.Equal(PrizeStatus.Collected, prize.Status);
    }

    [Fact]
    public void In_stock_prize_should_not_be_collectable()
    {
        var prize = new Prize("Påskeegg XL", 250);

        Assert.Throws<InvalidOperationException>(() => prize.Collect());
    }

    [Fact]
    public void Collected_prize_should_not_be_collectable_again()
    {
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();
        prize.Collect();

        Assert.Throws<InvalidOperationException>(() => prize.Collect());
    }

    [Fact]
    public void Collected_prize_should_not_be_deletable()
    {
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();
        prize.Collect();

        Assert.False(prize.CanDelete());
    }

    [Fact]
    public void In_stock_prize_should_be_deletable()
    {
        var prize = new Prize("Påskeegg XL", 250);

        Assert.True(prize.CanDelete());
    }

    [Fact]
    public void Assigned_prize_should_be_deletable()
    {
        var prize = new Prize("Påskeegg XL", 250);

        prize.Assign();

        Assert.True(prize.CanDelete());
    }

    [Fact]
    public void Prize_should_not_allow_name_shorter_than_2_characters()
    {
        Assert.Throws<ArgumentException>(() => new Prize("A", 250));
    }

    [Fact]
    public void Prize_should_not_allow_name_longer_than_80_characters()
    {
        var name = new String('A', 81);

        Assert.Throws<ArgumentException>(() => new Prize(name, 250));
    }
}
