namespace ExpectationEngine.API.Models;

public class News
{
    public int Id { get; set; }
    public int TickerId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public string? Source { get; set; }
    public DateTime PublishedAt { get; set; }
    public string? Url { get; set; }
    public decimal? SentimentScore { get; set; }
    public string? SentimentLabel { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Ticker Ticker { get; set; } = null!;
}
