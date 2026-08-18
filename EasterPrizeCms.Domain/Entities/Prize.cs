using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Domain.Entities;

public class Prize
{
    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public PrizeStatus Status { get; private set; }
    public int? ParticipantId { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }

    public Prize()
    {
        Status = PrizeStatus.InStock;
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = CreatedAtUtc;
    }

    public Prize(string name)
        : this(name, 0) { }

    public Prize(string name, decimal value)
    {
        ValidateName(name);
        ValidateValue(value);

        Name = name;
        Value = value;
        Status = PrizeStatus.InStock;
        CreatedAtUtc = DateTime.UtcNow;
        UpdatedAtUtc = CreatedAtUtc;
    }

    public void Update(string name, decimal value)
    {
        ValidateName(name);
        ValidateValue(value);

        Name = name;
        Value = value;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Assign()
    {
        if (Status != PrizeStatus.InStock)
            throw new InvalidOperationException("Prize can only be assigned when it is in stock.");

        Status = PrizeStatus.Assigned;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Assign(int participantId)
    {
        if (Status != PrizeStatus.InStock)
            throw new InvalidOperationException("Prize can only be assigned when it is in stock.");

        if (participantId <= 0)
            throw new ArgumentException(
                "Participant ID must be greater than 0.",
                nameof(participantId)
            );

        ParticipantId = participantId;
        Status = PrizeStatus.Assigned;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Collect()
    {
        if (Status != PrizeStatus.Assigned)
            throw new InvalidOperationException("Prize can only be collected when assigned.");

        Status = PrizeStatus.Collected;
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public bool CanDelete()
    {
        return Status != PrizeStatus.Collected;
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Prize name cannot be empty.", nameof(name));

        if (name.Length < 2 || name.Length > 80)
            throw new ArgumentException(
                "Prize name must be between 2 and 80 characters.",
                nameof(name)
            );
    }

    private static void ValidateValue(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Prize value cannot be negative.", nameof(value));
    }
}
