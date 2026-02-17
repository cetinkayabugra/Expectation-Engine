namespace ExpectationEngine.API.Models;

public class Price
{
    public int Id { get; set; }
    public int TickerId { get; set; }
    public DateTime Date { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal HighPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public decimal AdjustedClose { get; set; }
    public long Volume { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Ticker Ticker { get; set; } = null!;
}
