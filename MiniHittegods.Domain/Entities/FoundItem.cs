using MiniHittegods.Domain.Enums;

namespace MiniHittegods.Domain.Entities;

public class FoundItem
{
    public FoundItemStatus Status { get; private set; } = FoundItemStatus.Available;

    public DateTime FoundAtUtc { get; } = DateTime.UtcNow;

    public void Claim(string claimedBy)
    {
        if (Status != FoundItemStatus.Available)
        {
            throw new InvalidOperationException("Item is not available.");
        }

        Status = FoundItemStatus.Claimed;
    }
}