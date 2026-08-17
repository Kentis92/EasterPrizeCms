namespace EasterPrizeCms.Application.DTOs;

public class CreatePrizeRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}