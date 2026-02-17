using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface IEarningService
{
    Task<IEnumerable<Earning>> GetEarningsByTickerIdAsync(int tickerId);
    Task<Earning?> GetEarningByIdAsync(int id);
    Task<Earning> CreateEarningAsync(Earning earning);
    Task<Earning?> UpdateEarningAsync(int id, Earning earning);
}
