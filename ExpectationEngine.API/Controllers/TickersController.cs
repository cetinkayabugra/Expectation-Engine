using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TickersController : ControllerBase
{
    private readonly ITickerService _tickerService;

    public TickersController(ITickerService tickerService)
    {
        _tickerService = tickerService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Ticker>>> GetTickers()
    {
        var tickers = await _tickerService.GetAllTickersAsync();
        return Ok(tickers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Ticker>> GetTicker(int id)
    {
        var ticker = await _tickerService.GetTickerByIdAsync(id);
        if (ticker == null)
            return NotFound();

        return Ok(ticker);
    }

    [HttpGet("symbol/{symbol}")]
    public async Task<ActionResult<Ticker>> GetTickerBySymbol(string symbol)
    {
        var ticker = await _tickerService.GetTickerBySymbolAsync(symbol);
        if (ticker == null)
            return NotFound();

        return Ok(ticker);
    }

    [HttpPost]
    public async Task<ActionResult<Ticker>> CreateTicker(Ticker ticker)
    {
        var createdTicker = await _tickerService.CreateTickerAsync(ticker);
        return CreatedAtAction(nameof(GetTicker), new { id = createdTicker.Id }, createdTicker);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Ticker>> UpdateTicker(int id, Ticker ticker)
    {
        var updatedTicker = await _tickerService.UpdateTickerAsync(id, ticker);
        if (updatedTicker == null)
            return NotFound();

        return Ok(updatedTicker);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTicker(int id)
    {
        var result = await _tickerService.DeleteTickerAsync(id);
        if (!result)
            return NotFound();

        return NoContent();
    }
}
