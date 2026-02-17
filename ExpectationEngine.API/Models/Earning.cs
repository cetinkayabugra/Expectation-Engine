namespace ExpectationEngine.API.Models;

public class Earning
{
    public int Id { get; set; }
    public int TickerId { get; set; }
    public int FiscalYear { get; set; }
    public int FiscalQuarter { get; set; }
    public DateTime ReportDate { get; set; }
    public decimal? Revenue { get; set; }
    public decimal? NetIncome { get; set; }
    public decimal? EPS { get; set; }
    public decimal? EPSEstimate { get; set; }
    public decimal? RevenueSurprise { get; set; }
    public decimal? EPSSurprise { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Ticker Ticker { get; set; } = null!;
    public ICollection<Transcript> Transcripts { get; set; } = new List<Transcript>();
}
