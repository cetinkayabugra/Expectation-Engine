using ExpectationEngine.API.Data;
using ExpectationEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpectationEngine.API.Services.Implementations;

public class EarningService : IEarningService
{
    private readonly ExpectationEngineDbContext _context;

    public EarningService(ExpectationEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Earning>> GetEarningsByTickerIdAsync(int tickerId)
    {
        return await _context.Earnings
            .Where(e => e.TickerId == tickerId)
            .OrderByDescending(e => e.ReportDate)
            .ToListAsync();
    }

    public async Task<Earning?> GetEarningByIdAsync(int id)
    {
        return await _context.Earnings.FindAsync(id);
    }

    public async Task<Earning> CreateEarningAsync(Earning earning)
    {
        earning.CreatedAt = DateTime.UtcNow;
        _context.Earnings.Add(earning);
        await _context.SaveChangesAsync();
        return earning;
    }

    public async Task<Earning?> UpdateEarningAsync(int id, Earning earning)
    {
        var existingEarning = await _context.Earnings.FindAsync(id);
        if (existingEarning == null)
            return null;

        existingEarning.ReportDate = earning.ReportDate;
        existingEarning.Revenue = earning.Revenue;
        existingEarning.NetIncome = earning.NetIncome;
        existingEarning.EPS = earning.EPS;
        existingEarning.EPSEstimate = earning.EPSEstimate;
        existingEarning.RevenueSurprise = earning.RevenueSurprise;
        existingEarning.EPSSurprise = earning.EPSSurprise;

        await _context.SaveChangesAsync();
        return existingEarning;
    }
}
