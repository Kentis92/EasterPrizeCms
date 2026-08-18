namespace EasterPrizeCms.Application.DTOs;

public class UpdatePrizeRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
}
