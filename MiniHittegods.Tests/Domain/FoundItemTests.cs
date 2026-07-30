using MiniHittegods.Domain.Entities;
using MiniHittegods.Domain.Enums;

namespace MiniHittegods.Tests.Domain;

public class FoundItemTests
{
    [Fact]
    public void New_item_should_have_status_available()
    {
        var item = new FoundItem();

        Assert.Equal(FoundItemStatus.Available, item.Status);
    }

    [Fact]
    public void New_item_should_set_found_time()
    {
        var before = DateTime.UtcNow;

        var item = new FoundItem();

        var after = DateTime.UtcNow;

        Assert.InRange(item.FoundAtUtc, before, after);
    }

    [Fact]
    public void Claim_should_change_status_to_claimed()
    {
        var item = new FoundItem();

        item.Claim("Ola Nordmann");

        Assert.Equal(FoundItemStatus.Claimed, item.Status);
    }

    [Fact]
    public void Cannot_claim_item_that_is_already_claimed()
    {
        var item = new FoundItem();

        item.Claim("Ola Nordmann");

        Assert.Throws<InvalidOperationException>(() =>
            item.Claim("Kari Nordmann"));
    }
}