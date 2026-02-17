using ExpectationEngine.API.Models;

namespace ExpectationEngine.API.Services;

public interface ITranscriptService
{
    Task<IEnumerable<Transcript>> GetTranscriptsByTickerIdAsync(int tickerId);
    Task<Transcript?> GetTranscriptByIdAsync(int id);
    Task<Transcript> CreateTranscriptAsync(Transcript transcript);
    Task<Transcript?> UpdateTranscriptAsync(int id, Transcript transcript);
}
