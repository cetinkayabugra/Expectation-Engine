namespace ExpectationEngine.API.Models;

public class Feature
{
    public int Id { get; set; }
    public int TickerId { get; set; }
    public DateTime FeatureDate { get; set; }
    public decimal? PriceReturn5D { get; set; }
    public decimal? PriceReturn20D { get; set; }
    public decimal? Volatility20D { get; set; }
    public long? Volume20DAvg { get; set; }
    public decimal? NewsSentiment7D { get; set; }
    public int? NewsCount7D { get; set; }
    public decimal? EarningsSurprise { get; set; }
    public decimal? TranscriptSentiment { get; set; }
    public decimal? MarketCapChange { get; set; }
    public decimal? SectorMomentum { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Ticker Ticker { get; set; } = null!;
}
