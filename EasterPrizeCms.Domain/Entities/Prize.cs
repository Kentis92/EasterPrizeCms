using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Domain.Entities;

public class Prize
{
    public string Name { get; }
    public PrizeStatus Status { get; }

    public Prize()
    {
        Status = PrizeStatus.InStock;
    }

    public Prize(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Prize name cannot be empty.", nameof(name));

        Name = name;
        Status = PrizeStatus.InStock;
    }
}