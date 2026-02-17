using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeaturesController : ControllerBase
{
    private readonly IFeatureService _featureService;

    public FeaturesController(IFeatureService featureService)
    {
        _featureService = featureService;
    }

    [HttpGet("ticker/{tickerId}")]
    public async Task<ActionResult<IEnumerable<Feature>>> GetFeaturesByTicker(
        int tickerId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var features = await _featureService.GetFeaturesByTickerIdAsync(tickerId, startDate, endDate);
        return Ok(features);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Feature>> GetFeature(int id)
    {
        var feature = await _featureService.GetFeatureByIdAsync(id);
        if (feature == null)
            return NotFound();

        return Ok(feature);
    }

    [HttpPost]
    public async Task<ActionResult<Feature>> CreateFeature(Feature feature)
    {
        var createdFeature = await _featureService.CreateFeatureAsync(feature);
        return CreatedAtAction(nameof(GetFeature), new { id = createdFeature.Id }, createdFeature);
    }

    [HttpPost("compute")]
    public async Task<ActionResult> ComputeFeatures([FromQuery] int tickerId, [FromQuery] DateTime date)
    {
        await _featureService.ComputeFeaturesAsync(tickerId, date);
        return Ok();
    }
}
