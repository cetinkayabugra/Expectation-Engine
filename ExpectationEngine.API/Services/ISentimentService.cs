namespace ExpectationEngine.API.Services;

public interface ISentimentService
{
    Task<SentimentResult> AnalyzeSentimentAsync(string text);
}

public class SentimentResult
{
    public string Label { get; set; } = string.Empty;
    public double Score { get; set; }
    public Dictionary<string, double> Scores { get; set; } = new();
}
