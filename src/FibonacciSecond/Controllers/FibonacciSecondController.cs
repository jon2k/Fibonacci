using Common.Contract;
using FibonacciSecond.Application.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FibonacciSecond.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FibonacciSecondController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<FibonacciSecondController> _logger;

    public FibonacciSecondController(IMediator mediator, ILogger<FibonacciSecondController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Action to sum two numbers
    /// </summary>
    /// <param name="messageRequest"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MessageRequestFib messageRequest)
    {
        try
        {
            var result = await _mediator.Send(new CalculateCommand(messageRequest));
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError("{Error}",e.Message);
            return BadRequest(e.Message);
        }
    }
}