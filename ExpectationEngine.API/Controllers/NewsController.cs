using ExpectationEngine.API.Models;
using ExpectationEngine.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpectationEngine.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NewsController : ControllerBase
{
    private readonly INewsService _newsService;

    public NewsController(INewsService newsService)
    {
        _newsService = newsService;
    }

    [HttpGet("ticker/{tickerId}")]
    public async Task<ActionResult<IEnumerable<News>>> GetNewsByTicker(
        int tickerId,
        [FromQuery] DateTime? startDate = null,
        [FromQuery] DateTime? endDate = null)
    {
        var news = await _newsService.GetNewsByTickerIdAsync(tickerId, startDate, endDate);
        return Ok(news);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<News>> GetNews(int id)
    {
        var news = await _newsService.GetNewsByIdAsync(id);
        if (news == null)
            return NotFound();

        return Ok(news);
    }

    [HttpPost]
    public async Task<ActionResult<News>> CreateNews(News news)
    {
        var createdNews = await _newsService.CreateNewsAsync(news);
        return CreatedAtAction(nameof(GetNews), new { id = createdNews.Id }, createdNews);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<News>> UpdateNews(int id, News news)
    {
        var updatedNews = await _newsService.UpdateNewsAsync(id, news);
        if (updatedNews == null)
            return NotFound();

        return Ok(updatedNews);
    }
}
