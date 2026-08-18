using EasterPrizeCms.Domain.Enums;

namespace EasterPrizeCms.Application.DTOs;

public class PrizeResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public PrizeStatus Status { get; set; }
    public int? ParticipantId { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}
