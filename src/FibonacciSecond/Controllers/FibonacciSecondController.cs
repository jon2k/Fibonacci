using Common;
using FibonacciSecond.Services;
using Microsoft.AspNetCore.Mvc;

namespace FibonacciSecond.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FibonacciSecondController : ControllerBase
{
    private readonly IMessagesBus _messagesBus;
    private readonly ICalculateSum _calculateSum;
    private readonly ILogger<FibonacciSecondController> _logger;

    public FibonacciSecondController(
        IMessagesBus messagesBus, 
        ICalculateSum calculateSum,
        ILogger<FibonacciSecondController> logger)
    {
        _messagesBus = messagesBus;
        _calculateSum = calculateSum;
        _logger = logger;
    }

    /// <summary>
    /// todo
    /// </summary>
    /// <param name="messageRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MessageRequestFib messageRequest)
    {
        try
        {
            var sum = _calculateSum.Sum(messageRequest);
            await _messagesBus.SendMessageAsync(sum, HttpContext.RequestAborted);
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
}