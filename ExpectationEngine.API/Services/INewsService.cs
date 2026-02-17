using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface INewsService
{
    Task<IEnumerable<News>> GetNewsByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null);
    Task<News?> GetNewsByIdAsync(int id);
    Task<News> CreateNewsAsync(News news);
    Task<News?> UpdateNewsAsync(int id, News news);
}
