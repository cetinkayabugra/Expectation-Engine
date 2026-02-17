using ExpectationEngine.API.Data;
using ExpectationEngine.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpectationEngine.API.Services.Implementations;

public class TranscriptService : ITranscriptService
{
    private readonly ExpectationEngineDbContext _context;
    private readonly ISentimentService _sentimentService;

    public TranscriptService(ExpectationEngineDbContext context, ISentimentService sentimentService)
    {
        _context = context;
        _sentimentService = sentimentService;
    }

    public async Task<IEnumerable<Transcript>> GetTranscriptsByTickerIdAsync(int tickerId)
    {
        return await _context.Transcripts
            .Where(t => t.TickerId == tickerId)
            .OrderByDescending(t => t.TranscriptDate)
            .ToListAsync();
    }

    public async Task<Transcript?> GetTranscriptByIdAsync(int id)
    {
        return await _context.Transcripts.FindAsync(id);
    }

    public async Task<Transcript> CreateTranscriptAsync(Transcript transcript)
    {
        // Analyze sentiment
        var sentiment = await _sentimentService.AnalyzeSentimentAsync(transcript.TranscriptText);
        transcript.SentimentScore = (decimal)sentiment.Score;
        transcript.SentimentLabel = sentiment.Label;

        transcript.CreatedAt = DateTime.UtcNow;
        _context.Transcripts.Add(transcript);
        await _context.SaveChangesAsync();
        return transcript;
    }

    public async Task<Transcript?> UpdateTranscriptAsync(int id, Transcript transcript)
    {
        var existingTranscript = await _context.Transcripts.FindAsync(id);
        if (existingTranscript == null)
            return null;

        existingTranscript.TranscriptText = transcript.TranscriptText;
        existingTranscript.TranscriptDate = transcript.TranscriptDate;
        existingTranscript.KeyPhrases = transcript.KeyPhrases;

        // Re-analyze sentiment
        var sentiment = await _sentimentService.AnalyzeSentimentAsync(transcript.TranscriptText);
        existingTranscript.SentimentScore = (decimal)sentiment.Score;
        existingTranscript.SentimentLabel = sentiment.Label;

        await _context.SaveChangesAsync();
        return existingTranscript;
    }
}
