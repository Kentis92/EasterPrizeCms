namespace EasterPrizeCms.Application.DTOs;

public class ParticipantResponse
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string City { get; set; } = string.Empty;
    public DateTime CreatedAtUtc  { get; set; }
    public DateTime UpdatedAtUtc { get; set; }
}