using MiniHittegods.Domain.Enums;

namespace MiniHittegods.Domain.Entities;

public class FoundItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public FoundItemStatus Status { get; private set; } = FoundItemStatus.Available;

    public DateTime FoundAtUtc { get; private set; } = DateTime.UtcNow;

    public DateTime? ReturnedAtUtc { get; private set; }

    public void Claim(string claimedBy)
    {
        if (Status != FoundItemStatus.Available)
        {
            throw new InvalidOperationException("Item has already been claimed.");
        }

        Status = FoundItemStatus.Claimed;
    }

    public void Return()
    {
        if (Status != FoundItemStatus.Claimed)
        {
            throw new InvalidOperationException("Only claimed items can be returned.");
        }

        Status = FoundItemStatus.Returned;
        ReturnedAtUtc = DateTime.UtcNow;
    }
}