using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranscriptsController : ControllerBase
{
    private readonly ITranscriptService _transcriptService;

    public TranscriptsController(ITranscriptService transcriptService)
    {
        _transcriptService = transcriptService;
    }

    [HttpGet("ticker/{tickerId}")]
    public async Task<ActionResult<IEnumerable<Transcript>>> GetTranscriptsByTicker(int tickerId)
    {
        var transcripts = await _transcriptService.GetTranscriptsByTickerIdAsync(tickerId);
        return Ok(transcripts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Transcript>> GetTranscript(int id)
    {
        var transcript = await _transcriptService.GetTranscriptByIdAsync(id);
        if (transcript == null)
            return NotFound();

        return Ok(transcript);
    }

    [HttpPost]
    public async Task<ActionResult<Transcript>> CreateTranscript(Transcript transcript)
    {
        var createdTranscript = await _transcriptService.CreateTranscriptAsync(transcript);
        return CreatedAtAction(nameof(GetTranscript), new { id = createdTranscript.Id }, createdTranscript);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Transcript>> UpdateTranscript(int id, Transcript transcript)
    {
        var updatedTranscript = await _transcriptService.UpdateTranscriptAsync(id, transcript);
        if (updatedTranscript == null)
            return NotFound();

        return Ok(updatedTranscript);
    }
}
