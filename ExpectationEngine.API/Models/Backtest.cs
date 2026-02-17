namespace ExpectationEngine.API.Models;

public class Backtest
{
    public int Id { get; set; }
    public string BacktestName { get; set; } = string.Empty;
    public string ModelVersion { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal? TotalReturn { get; set; }
    public decimal? SharpeRatio { get; set; }
    public decimal? MaxDrawdown { get; set; }
    public decimal? WinRate { get; set; }
    public int? TotalTrades { get; set; }
    public decimal? AvgTradeReturn { get; set; }
    public string? Parameters { get; set; }
    public string? Results { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
