using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BacktestsController : ControllerBase
{
    private readonly IBacktestService _backtestService;

    public BacktestsController(IBacktestService backtestService)
    {
        _backtestService = backtestService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Backtest>>> GetBacktests()
    {
        var backtests = await _backtestService.GetAllBacktestsAsync();
        return Ok(backtests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Backtest>> GetBacktest(int id)
    {
        var backtest = await _backtestService.GetBacktestByIdAsync(id);
        if (backtest == null)
            return NotFound();

        return Ok(backtest);
    }

    [HttpPost]
    public async Task<ActionResult<Backtest>> CreateBacktest(Backtest backtest)
    {
        var createdBacktest = await _backtestService.CreateBacktestAsync(backtest);
        return CreatedAtAction(nameof(GetBacktest), new { id = createdBacktest.Id }, createdBacktest);
    }

    [HttpPost("run")]
    public async Task<ActionResult<Backtest>> RunBacktest(
        [FromQuery] string backtestName,
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate,
        [FromQuery] string modelVersion)
    {
        var backtest = await _backtestService.RunBacktestAsync(backtestName, startDate, endDate, modelVersion);
        return Ok(backtest);
    }
}
