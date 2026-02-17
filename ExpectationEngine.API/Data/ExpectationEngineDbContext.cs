using Microsoft.EntityFrameworkCore;
using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Data;

public class ExpectationEngineDbContext : DbContext
{
    public ExpectationEngineDbContext(DbContextOptions<ExpectationEngineDbContext> options)
        : base(options)
    {
    }

    public DbSet<Ticker> Tickers { get; set; }
    public DbSet<Price> Prices { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Earning> Earnings { get; set; }
    public DbSet<Transcript> Transcripts { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<Prediction> Predictions { get; set; }
    public DbSet<Backtest> Backtests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Ticker configuration
        modelBuilder.Entity<Ticker>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Symbol).IsUnique();
            entity.Property(e => e.Symbol).HasMaxLength(10).IsRequired();
            entity.Property(e => e.CompanyName).HasMaxLength(255).IsRequired();
            entity.Property(e => e.Sector).HasMaxLength(100);
            entity.Property(e => e.Industry).HasMaxLength(100);
            entity.Property(e => e.Exchange).HasMaxLength(50);
        });

        // Price configuration
        modelBuilder.Entity<Price>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.TickerId, e.Date }).IsUnique();
            entity.Property(e => e.OpenPrice).HasPrecision(18, 4);
            entity.Property(e => e.HighPrice).HasPrecision(18, 4);
            entity.Property(e => e.LowPrice).HasPrecision(18, 4);
            entity.Property(e => e.ClosePrice).HasPrecision(18, 4);
            entity.Property(e => e.AdjustedClose).HasPrecision(18, 4);
            entity.HasOne(e => e.Ticker)
                .WithMany(t => t.Prices)
                .HasForeignKey(e => e.TickerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // News configuration
        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.TickerId, e.PublishedAt });
            entity.HasIndex(e => e.PublishedAt);
            entity.Property(e => e.Title).HasMaxLength(500).IsRequired();
            entity.Property(e => e.Source).HasMaxLength(100);
            entity.Property(e => e.Url).HasMaxLength(500);
            entity.Property(e => e.SentimentScore).HasPrecision(5, 4);
            entity.Property(e => e.SentimentLabel).HasMaxLength(20);
            entity.HasOne(e => e.Ticker)
                .WithMany(t => t.News)
                .HasForeignKey(e => e.TickerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Earning configuration
        modelBuilder.Entity<Earning>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.TickerId, e.FiscalYear, e.FiscalQuarter }).IsUnique();
            entity.HasIndex(e => new { e.TickerId, e.ReportDate });
            entity.Property(e => e.Revenue).HasPrecision(18, 2);
            entity.Property(e => e.NetIncome).HasPrecision(18, 2);
            entity.Property(e => e.EPS).HasPrecision(10, 4);
            entity.Property(e => e.EPSEstimate).HasPrecision(10, 4);
            entity.Property(e => e.RevenueSurprise).HasPrecision(10, 4);
            entity.Property(e => e.EPSSurprise).HasPrecision(10, 4);
            entity.HasOne(e => e.Ticker)
                .WithMany(t => t.Earnings)
                .HasForeignKey(e => e.TickerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Transcript configuration
        modelBuilder.Entity<Transcript>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.TickerId);
            entity.Property(e => e.TranscriptText).IsRequired();
            entity.Property(e => e.SentimentScore).HasPrecision(5, 4);
            entity.Property(e => e.SentimentLabel).HasMaxLength(20);
            entity.HasOne(e => e.Earnings)
                .WithMany(ea => ea.Transcripts)
                .HasForeignKey(e => e.EarningsId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(e => e.Ticker)
                .WithMany()
                .HasForeignKey(e => e.TickerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Feature configuration
        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.TickerId, e.FeatureDate }).IsUnique();
            entity.Property(e => e.PriceReturn5D).HasPrecision(10, 6);
            entity.Property(e => e.PriceReturn20D).HasPrecision(10, 6);
            entity.Property(e => e.Volatility20D).HasPrecision(10, 6);
            entity.Property(e => e.NewsSentiment7D).HasPrecision(5, 4);
            entity.Property(e => e.EarningsSurprise).HasPrecision(10, 4);
            entity.Property(e => e.TranscriptSentiment).HasPrecision(5, 4);
            entity.Property(e => e.MarketCapChange).HasPrecision(18, 2);
            entity.Property(e => e.SectorMomentum).HasPrecision(10, 6);
            entity.HasOne(e => e.Ticker)
                .WithMany(t => t.Features)
                .HasForeignKey(e => e.TickerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Prediction configuration
        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.TickerId, e.PredictionDate });
            entity.Property(e => e.PredictedReturn).HasPrecision(10, 6);
            entity.Property(e => e.Confidence).HasPrecision(5, 4);
            entity.Property(e => e.ModelVersion).HasMaxLength(50);
            entity.Property(e => e.ActualReturn).HasPrecision(10, 6);
            entity.HasOne(e => e.Ticker)
                .WithMany(t => t.Predictions)
                .HasForeignKey(e => e.TickerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Backtest configuration
        modelBuilder.Entity<Backtest>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ModelVersion);
            entity.Property(e => e.BacktestName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.ModelVersion).HasMaxLength(50).IsRequired();
            entity.Property(e => e.TotalReturn).HasPrecision(10, 6);
            entity.Property(e => e.SharpeRatio).HasPrecision(10, 6);
            entity.Property(e => e.MaxDrawdown).HasPrecision(10, 6);
            entity.Property(e => e.WinRate).HasPrecision(5, 4);
            entity.Property(e => e.AvgTradeReturn).HasPrecision(10, 6);
        });
    }
}
