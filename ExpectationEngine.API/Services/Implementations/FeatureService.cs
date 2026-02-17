using ExpectationEngine.API.Data;
using ExpectationEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpectationEngine.API.Services.Implementations;

public class FeatureService : IFeatureService
{
    private readonly ExpectationEngineDbContext _context;

    public FeatureService(ExpectationEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Feature>> GetFeaturesByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Features.Where(f => f.TickerId == tickerId);

        if (startDate.HasValue)
            query = query.Where(f => f.FeatureDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(f => f.FeatureDate <= endDate.Value);

        return await query.OrderBy(f => f.FeatureDate).ToListAsync();
    }

    public async Task<Feature?> GetFeatureByIdAsync(int id)
    {
        return await _context.Features.FindAsync(id);
    }

    public async Task<Feature> CreateFeatureAsync(Feature feature)
    {
        feature.CreatedAt = DateTime.UtcNow;
        _context.Features.Add(feature);
        await _context.SaveChangesAsync();
        return feature;
    }

    public async Task ComputeFeaturesAsync(int tickerId, DateTime date)
    {
        // Stub implementation - compute features from prices, news, earnings
        // This would typically calculate:
        // - Price returns over different periods
        // - Volatility measures
        // - News sentiment aggregates
        // - Earnings surprises
        // - Volume trends
        
        var feature = new Feature
        {
            TickerId = tickerId,
            FeatureDate = date,
            PriceReturn5D = 0.0m,  // Stub: would calculate from Price table
            PriceReturn20D = 0.0m, // Stub: would calculate from Price table
            Volatility20D = 0.0m,  // Stub: would calculate from Price table
            Volume20DAvg = 0,      // Stub: would calculate from Price table
            NewsSentiment7D = 0.0m, // Stub: would aggregate from News table
            NewsCount7D = 0,        // Stub: would count from News table
            CreatedAt = DateTime.UtcNow
        };

        _context.Features.Add(feature);
        await _context.SaveChangesAsync();
    }
}
