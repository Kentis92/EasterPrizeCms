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

    [Fact]
    public void Claimed_item_can_be_returned()
    {
        var item = new FoundItem();

        item.Claim("Ola");

        item.Return();

        Assert.Equal(FoundItemStatus.Returned, item.Status);
        Assert.NotNull(item.ReturnedAtUtc);
    }

[Fact]
public void New_item_should_store_basic_information()
    {
        var item = new FoundItem(
            "Blå jakke",
            "Norge skrevet på ryggen",
            "Clothing",
            "Scene 2"
        );

        Assert.Equal("Blå jakke", item.Title);
        Assert.Equal("Norge skrevet på ryggen", item.Description);
        Assert.Equal("Clothing", item.Category);
        Assert.Equal("Scene 2", item.FoundLocation);
    }

[Fact]
public void Claim_should_store_claim_information()
    {
        var item = new FoundItem();

        item.Claim("Ola Nordmann");

        Assert.Equal("Ola Nordmann", item.ClaimedBy);
        Assert.NotNull(item.ClaimedAtUtc);
    }

[Fact]
public void Available_item_can_be_deleted()
    {
        var item = new FoundItem();

        Assert.True(item.CanBeDeleted());
    }

[Fact]
public void Claimed_item_cannot_be_deleted()
    {
        var item = new FoundItem();

        item.Claim("Ola Nordmann");

        Assert.False(item.CanBeDeleted());
    }


}