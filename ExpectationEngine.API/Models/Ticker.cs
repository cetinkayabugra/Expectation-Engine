namespace ExpectationEngine.API.Models;

public class Ticker
{
    public int Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string? Sector { get; set; }
    public string? Industry { get; set; }
    public string? Exchange { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Price> Prices { get; set; } = new List<Price>();
    public ICollection<News> News { get; set; } = new List<News>();
    public ICollection<Earning> Earnings { get; set; } = new List<Earning>();
    public ICollection<Feature> Features { get; set; } = new List<Feature>();
    public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}
