namespace EasterPrizeCms.Application.DTOs;

public class CreateParticipantRequest
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string City { get; set; } = string.Empty;
}