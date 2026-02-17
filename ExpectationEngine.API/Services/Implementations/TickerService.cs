using ExpectationEngine.API.Data;
using ExpectationEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpectationEngine.API.Services.Implementations;

public class TickerService : ITickerService
{
    private readonly ExpectationEngineDbContext _context;

    public TickerService(ExpectationEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Ticker>> GetAllTickersAsync()
    {
        return await _context.Tickers
            .Where(t => t.IsActive)
            .OrderBy(t => t.Symbol)
            .ToListAsync();
    }

    public async Task<Ticker?> GetTickerByIdAsync(int id)
    {
        return await _context.Tickers.FindAsync(id);
    }

    public async Task<Ticker?> GetTickerBySymbolAsync(string symbol)
    {
        return await _context.Tickers
            .FirstOrDefaultAsync(t => t.Symbol == symbol);
    }

    public async Task<Ticker> CreateTickerAsync(Ticker ticker)
    {
        ticker.CreatedAt = DateTime.UtcNow;
        ticker.UpdatedAt = DateTime.UtcNow;
        _context.Tickers.Add(ticker);
        await _context.SaveChangesAsync();
        return ticker;
    }

    public async Task<Ticker?> UpdateTickerAsync(int id, Ticker ticker)
    {
        var existingTicker = await _context.Tickers.FindAsync(id);
        if (existingTicker == null)
            return null;

        existingTicker.CompanyName = ticker.CompanyName;
        existingTicker.Sector = ticker.Sector;
        existingTicker.Industry = ticker.Industry;
        existingTicker.Exchange = ticker.Exchange;
        existingTicker.IsActive = ticker.IsActive;
        existingTicker.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingTicker;
    }

    public async Task<bool> DeleteTickerAsync(int id)
    {
        var ticker = await _context.Tickers.FindAsync(id);
        if (ticker == null)
            return false;

        ticker.IsActive = false;
        ticker.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }
}
