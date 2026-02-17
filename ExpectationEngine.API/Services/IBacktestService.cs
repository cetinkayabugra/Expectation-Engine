using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface IBacktestService
{
    Task<IEnumerable<Backtest>> GetAllBacktestsAsync();
    Task<Backtest?> GetBacktestByIdAsync(int id);
    Task<Backtest> CreateBacktestAsync(Backtest backtest);
    Task<Backtest> RunBacktestAsync(string backtestName, DateTime startDate, DateTime endDate, string modelVersion);
}
