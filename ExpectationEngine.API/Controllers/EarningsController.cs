using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EarningsController : ControllerBase
{
    private readonly IEarningService _earningService;

    public EarningsController(IEarningService earningService)
    {
        _earningService = earningService;
    }

    [HttpGet("ticker/{tickerId}")]
    public async Task<ActionResult<IEnumerable<Earning>>> GetEarningsByTicker(int tickerId)
    {
        var earnings = await _earningService.GetEarningsByTickerIdAsync(tickerId);
        return Ok(earnings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Earning>> GetEarning(int id)
    {
        var earning = await _earningService.GetEarningByIdAsync(id);
        if (earning == null)
            return NotFound();

        return Ok(earning);
    }

    [HttpPost]
    public async Task<ActionResult<Earning>> CreateEarning(Earning earning)
    {
        var createdEarning = await _earningService.CreateEarningAsync(earning);
        return CreatedAtAction(nameof(GetEarning), new { id = createdEarning.Id }, createdEarning);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Earning>> UpdateEarning(int id, Earning earning)
    {
        var updatedEarning = await _earningService.UpdateEarningAsync(id, earning);
        if (updatedEarning == null)
            return NotFound();

        return Ok(updatedEarning);
    }
}
