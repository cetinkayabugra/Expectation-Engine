namespace ExpectationEngine.API.Models;

public class Transcript
{
    public int Id { get; set; }
    public int EarningsId { get; set; }
    public int TickerId { get; set; }
    public string TranscriptText { get; set; } = string.Empty;
    public DateTime TranscriptDate { get; set; }
    public decimal? SentimentScore { get; set; }
    public string? SentimentLabel { get; set; }
    public string? KeyPhrases { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Earning Earnings { get; set; } = null!;
    public Ticker Ticker { get; set; } = null!;
}
