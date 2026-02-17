using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface IPriceService
{
    Task<IEnumerable<Price>> GetPricesByTickerIdAsync(int tickerId, DateTime? startDate = null, DateTime? endDate = null);
    Task<Price?> GetPriceByIdAsync(int id);
    Task<Price> CreatePriceAsync(Price price);
    Task<IEnumerable<Price>> CreatePricesAsync(IEnumerable<Price> prices);
}
