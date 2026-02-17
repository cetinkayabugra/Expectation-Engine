namespace ExpectationEngine.API.Models;

public class Prediction
{
    public int Id { get; set; }
    public int TickerId { get; set; }
    public DateTime PredictionDate { get; set; }
    public DateTime TargetDate { get; set; }
    public decimal PredictedReturn { get; set; }
    public decimal? Confidence { get; set; }
    public string? ModelVersion { get; set; }
    public string? FeatureSnapshot { get; set; }
    public decimal? ActualReturn { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Ticker Ticker { get; set; } = null!;
}
