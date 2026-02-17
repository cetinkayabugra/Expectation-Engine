using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface IPredictionService
{
    Task<IEnumerable<Prediction>> GetPredictionsByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null);
    Task<Prediction?> GetPredictionByIdAsync(int id);
    Task<Prediction> CreatePredictionAsync(Prediction prediction);
    Task<Prediction> GeneratePredictionAsync(int tickerId, DateTime targetDate);
}
