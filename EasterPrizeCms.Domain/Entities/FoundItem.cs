using MiniHittegods.Domain.Enums;

namespace MiniHittegods.Domain.Entities;

public class FoundItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public string Category { get; private set; } = string.Empty;
    public string FoundLocation { get; private set; } = string.Empty;

    public FoundItemStatus Status { get; private set; } = FoundItemStatus.Available;

    public DateTime FoundAtUtc { get; private set; } = DateTime.UtcNow;

    public string? ClaimedBy { get; private set; }
    public DateTime? ClaimedAtUtc { get; private set; }

    public DateTime? ReturnedAtUtc { get; private set; }

    public FoundItem()
    {
    }

    public FoundItem(
        string title,
        string? description,
        string category,
        string foundLocation)
    {
        Title = title;
        Description = description;
        Category = category;
        FoundLocation = foundLocation;
    }

    public void Claim(string claimedBy)
    {
        if (Status != FoundItemStatus.Available)
        {
            throw new InvalidOperationException("Item has already been claimed.");
        }

        Status = FoundItemStatus.Claimed;
        ClaimedBy = claimedBy;
        ClaimedAtUtc = DateTime.UtcNow;
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

    public bool CanBeDeleted()
    {
        return Status == FoundItemStatus.Available;
    }
}