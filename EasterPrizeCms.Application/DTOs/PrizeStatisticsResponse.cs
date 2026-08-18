namespace EasterPrizeCms.Application.DTOs;

public class PrizeStatisticsResponse
{
    public int TotalPrizes { get; set; }
    public int InStock { get; set; }
    public int Assigned { get; set; }
    public int Collected { get; set; }
    public decimal TotalValue { get; set; }
    public decimal AverageValue { get; set; }
}
