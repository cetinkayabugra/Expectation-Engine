using ExpectationEngine.API.Data;
using ExpectationEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpectationEngine.API.Services.Implementations;

public class PredictionService : IPredictionService
{
    private readonly ExpectationEngineDbContext _context;

    public PredictionService(ExpectationEngineDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prediction>> GetPredictionsByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Predictions.Where(p => p.TickerId == tickerId);

        if (startDate.HasValue)
            query = query.Where(p => p.PredictionDate >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(p => p.PredictionDate <= endDate.Value);

        return await query.OrderByDescending(p => p.PredictionDate).ToListAsync();
    }

    public async Task<Prediction?> GetPredictionByIdAsync(int id)
    {
        return await _context.Predictions.FindAsync(id);
    }

    public async Task<Prediction> CreatePredictionAsync(Prediction prediction)
    {
        prediction.CreatedAt = DateTime.UtcNow;
        _context.Predictions.Add(prediction);
        await _context.SaveChangesAsync();
        return prediction;
    }

    public async Task<Prediction> GeneratePredictionAsync(int tickerId, DateTime targetDate)
    {
        // Stub implementation - would use ML model to generate prediction
        // This would typically:
        // 1. Fetch latest features for the ticker
        // 2. Load ML model
        // 3. Generate prediction
        // 4. Store prediction with confidence score

        var prediction = new Prediction
        {
            TickerId = tickerId,
            PredictionDate = DateTime.UtcNow,
            TargetDate = targetDate,
            PredictedReturn = 0.0m,  // Stub: would come from ML model
            Confidence = 0.5m,        // Stub: would come from ML model
            ModelVersion = "v1.0",
            FeatureSnapshot = "{}", // Stub: would serialize features used
            CreatedAt = DateTime.UtcNow
        };

        _context.Predictions.Add(prediction);
        await _context.SaveChangesAsync();
        return prediction;
    }
}
