using ExpectationEngine.API.Data;
using ExpectationEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpectationEngine.API.Services.Implementations;

public class NewsService : INewsService
{
    private readonly ExpectationEngineDbContext _context;
    private readonly ISentimentService _sentimentService;

    public NewsService(ExpectationEngineDbContext context, ISentimentService sentimentService)
    {
        _context = context;
        _sentimentService = sentimentService;
    }

    public async Task<IEnumerable<News>> GetNewsByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.News.Where(n => n.TickerId == tickerId);

        if (startDate.HasValue)
            query = query.Where(n => n.PublishedAt >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(n => n.PublishedAt <= endDate.Value);

        return await query.OrderByDescending(n => n.PublishedAt).ToListAsync();
    }

    public async Task<News?> GetNewsByIdAsync(int id)
    {
        return await _context.News.FindAsync(id);
    }

    public async Task<News> CreateNewsAsync(News news)
    {
        // Analyze sentiment if content is provided
        if (!string.IsNullOrEmpty(news.Content))
        {
            var sentiment = await _sentimentService.AnalyzeSentimentAsync(news.Content);
            news.SentimentScore = (decimal)sentiment.Score;
            news.SentimentLabel = sentiment.Label;
        }

        news.CreatedAt = DateTime.UtcNow;
        _context.News.Add(news);
        await _context.SaveChangesAsync();
        return news;
    }

    public async Task<News?> UpdateNewsAsync(int id, News news)
    {
        var existingNews = await _context.News.FindAsync(id);
        if (existingNews == null)
            return null;

        existingNews.Title = news.Title;
        existingNews.Content = news.Content;
        existingNews.Source = news.Source;
        existingNews.PublishedAt = news.PublishedAt;
        existingNews.Url = news.Url;

        // Re-analyze sentiment if content changed
        if (!string.IsNullOrEmpty(news.Content))
        {
            var sentiment = await _sentimentService.AnalyzeSentimentAsync(news.Content);
            existingNews.SentimentScore = (decimal)sentiment.Score;
            existingNews.SentimentLabel = sentiment.Label;
        }

        await _context.SaveChangesAsync();
        return existingNews;
    }
}
