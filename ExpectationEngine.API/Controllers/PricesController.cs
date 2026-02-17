using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PricesController : ControllerBase
{
    private readonly IPriceService _priceService;

    public PricesController(IPriceService priceService)
    {
        _priceService = priceService;
    }

    [HttpGet("ticker/{tickerId}")]
    public async Task<ActionResult<IEnumerable<Price>>> GetPricesByTicker(
        int tickerId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var prices = await _priceService.GetPricesByTickerIdAsync(tickerId, startDate, endDate);
        return Ok(prices);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Price>> GetPrice(int id)
    {
        var price = await _priceService.GetPriceByIdAsync(id);
        if (price == null)
            return NotFound();

        return Ok(price);
    }

    [HttpPost]
    public async Task<ActionResult<Price>> CreatePrice(Price price)
    {
        var createdPrice = await _priceService.CreatePriceAsync(price);
        return CreatedAtAction(nameof(GetPrice), new { id = createdPrice.Id }, createdPrice);
    }

    [HttpPost("batch")]
    public async Task<ActionResult<IEnumerable<Price>>> CreatePrices(IEnumerable<Price> prices)
    {
        var createdPrices = await _priceService.CreatePricesAsync(prices);
        return Ok(createdPrices);
    }
}
