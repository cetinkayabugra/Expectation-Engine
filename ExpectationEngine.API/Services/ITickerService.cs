using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface ITickerService
{
    Task<IEnumerable<Ticker>> GetAllTickersAsync();
    Task<Ticker?> GetTickerByIdAsync(int id);
    Task<Ticker?> GetTickerBySymbolAsync(string symbol);
    Task<Ticker> CreateTickerAsync(Ticker ticker);
    Task<Ticker?> UpdateTickerAsync(int id, Ticker ticker);
    Task<bool> DeleteTickerAsync(int id);
}
