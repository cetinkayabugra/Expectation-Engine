using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PredictionsController : ControllerBase
{
    private readonly IPredictionService _predictionService;

    public PredictionsController(IPredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    [HttpGet("ticker/{tickerId}")]
    public async Task<ActionResult<IEnumerable<Prediction>>> GetPredictionsByTicker(
        int tickerId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var predictions = await _predictionService.GetPredictionsByTickerIdAsync(tickerId, startDate, endDate);
        return Ok(predictions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Prediction>> GetPrediction(int id)
    {
        var prediction = await _predictionService.GetPredictionByIdAsync(id);
        if (prediction == null)
            return NotFound();

        return Ok(prediction);
    }

    [HttpPost]
    public async Task<ActionResult<Prediction>> CreatePrediction(Prediction prediction)
    {
        var createdPrediction = await _predictionService.CreatePredictionAsync(prediction);
        return CreatedAtAction(nameof(GetPrediction), new { id = createdPrediction.Id }, createdPrediction);
    }

    [HttpPost("generate")]
    public async Task<ActionResult<Prediction>> GeneratePrediction([FromQuery] int tickerId, [FromQuery] DateTime targetDate)
    {
        var prediction = await _predictionService.GeneratePredictionAsync(tickerId, targetDate);
        return Ok(prediction);
    }
}
