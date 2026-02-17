using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface IFeatureService
{
    Task<IEnumerable<Feature>> GetFeaturesByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null);
    Task<Feature?> GetFeatureByIdAsync(int id);
    Task<Feature> CreateFeatureAsync(Feature feature);
    Task ComputeFeaturesAsync(int tickerId, DateTime date);
}
