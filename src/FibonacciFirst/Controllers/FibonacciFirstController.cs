using Fibonacci.Application.Command;
using Fibonacci.Application.Query;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FibonacciFirstController : ControllerBase
{
    private readonly IValidator<int> _validator;
    private readonly ILogger<FibonacciFirstController> _logger;
    private readonly IMediator _mediator;

    public FibonacciFirstController(IValidator<int> validator, ILogger<FibonacciFirstController> logger, IMediator mediator)
    {
        _validator = validator;
        _logger = logger;
        _mediator = mediator;
    }

    /// <summary>
    /// Action to get fibonacci numbers
    /// </summary>
    /// <param name="number">Amount of numbers</param>
    /// <returns>Returns a list of fibonacci numbers</returns>
    [HttpGet("{number}")]
    public async Task<IActionResult> Get(int number)
    {
        var validResult = await _validator.ValidateAsync(number);
        if (!validResult.IsValid)
        {
            return BadRequest("Number must be greater than 0 and less than 92");
        }

        try
        {
            var result = await _mediator.Send(new GetFibNumbersQuery(number));
            return Ok(result);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }

    /// <summary>
    /// Action to calculate fibonacci numbers
    /// </summary>
    /// <param name="number">Amount of numbers</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(int number)
    {
        var validResult = await _validator.ValidateAsync(number);
        if (!validResult.IsValid)
        {
            return BadRequest("Number must be greater than 0 and less than 92");
        }

        try
        {
            var result = await _mediator.Send(new StartCalculateCommand(number));
            return Ok(result.ToString());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest(e.Message);
        }
    }
}