namespace EasterPrizeCms.Application.DTOs;

public class UpdateParticipantRequest
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string City { get; set; } = string.Empty;
}
