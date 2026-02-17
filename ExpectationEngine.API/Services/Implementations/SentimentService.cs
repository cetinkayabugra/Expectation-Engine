namespace ExpectationEngine.API.Services.Implementations;

public class SentimentService : ISentimentService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public SentimentService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<SentimentResult> AnalyzeSentimentAsync(string text)
    {
        try
        {
            var nlpServiceUrl = _configuration["NlpServiceUrl"] ?? "http://nlp-service:8000";
            var response = await _httpClient.PostAsJsonAsync($"{nlpServiceUrl}/score", new { text });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<NlpResponse>();
                if (result != null)
                {
                    return new SentimentResult
                    {
                        Label = result.Label,
                        Score = result.Score,
                        Scores = result.Scores
                    };
                }
            }
        }
        catch (Exception)
        {
            // If NLP service is unavailable, return neutral sentiment
        }

        // Fallback to neutral sentiment
        return new SentimentResult
        {
            Label = "neutral",
            Score = 0.5,
            Scores = new Dictionary<string, double>
            {
                { "positive", 0.33 },
                { "negative", 0.33 },
                { "neutral", 0.34 }
            }
        };
    }

    private class NlpResponse
    {
        public string Label { get; set; } = string.Empty;
        public double Score { get; set; }
        public Dictionary<string, double> Scores { get; set; } = new();
    }
}
