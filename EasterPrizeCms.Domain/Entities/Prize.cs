using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Domain.Entities;

public class Prize
{
    public string Name { get; }
    public decimal Value { get; }
    public PrizeStatus Status { get; private set; }

    public Prize()
    {
        Status = PrizeStatus.InStock;
    }

    public Prize(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Prize name cannot be empty.", nameof(name));

        if (name.Length < 2 || name.Length > 80)
            throw new ArgumentException("Prize name must be between 2 and 80 characters.", nameof(name));

        Name = name;
        Status = PrizeStatus.InStock;
    }

    public Prize(string name, decimal value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Prize name cannot be empty.", nameof(name));

        if (name.Length < 2 || name.Length > 80)
            throw new ArgumentException("Prize name must be between 2 and 80 characters.", nameof(name));

        if (value < 0)
            throw new ArgumentException("Prize value cannot be negative.", nameof(value));

        Name = name;
        Value = value;
        Status = PrizeStatus.InStock;
    }

    public void Assign()
    {
        if (Status != PrizeStatus.InStock)
            throw new InvalidOperationException("Prize can only be assigned when it is in stock.");

        Status = PrizeStatus.Assigned;
    }

    public void Collect()
    {
        if (Status != PrizeStatus.Assigned)
            throw new InvalidOperationException("Prize can only be collected when assigned.");

        Status = PrizeStatus.Collected;
    }

    public bool CanDelete()
    {
        return Status != PrizeStatus.Collected;
    }
}