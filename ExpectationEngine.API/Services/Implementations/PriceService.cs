using ExpectationEngine.API.Data;
using ExpectationEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpectationEngine.API.Services.Implementations;

public class PriceService : IPriceService
{
    private readonly ExpectationEngineDbContext _context;

    public PriceService(ExpectationEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Price>> GetPricesByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Prices.Where(p => p.TickerId == tickerId);

        if (startDate.HasValue)
            query = query.Where(p => p.Date >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(p => p.Date <= endDate.Value);

        return await query.OrderBy(p => p.Date).ToListAsync();
    }

    public async Task<Price?> GetPriceByIdAsync(int id)
    {
        return await _context.Prices.FindAsync(id);
    }

    public async Task<Price> CreatePriceAsync(Price price)
    {
        price.CreatedAt = DateTime.UtcNow;
        _context.Prices.Add(price);
        await _context.SaveChangesAsync();
        return price;
    }

    public async Task<IEnumerable<Price>> CreatePricesAsync(IEnumerable<Price> prices)
    {
        var priceList = prices.ToList();
        foreach (var price in priceList)
        {
            price.CreatedAt = DateTime.UtcNow;
        }
        _context.Prices.AddRange(priceList);
        await _context.SaveChangesAsync();
        return priceList;
    }
}
