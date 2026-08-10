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
}