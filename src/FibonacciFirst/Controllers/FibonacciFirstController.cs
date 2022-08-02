using System.Net;
using Common;
using Fibonacci.Services;
using Fibonacci.Storage;
using Microsoft.AspNetCore.Mvc;

namespace Fibonacci.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FibonacciFirstController : ControllerBase
{
    private readonly IRepo _repo;
    private readonly ITaskListener _taskListener;
    private readonly IHttpClientService _httpClientService;
    private readonly ILogger<FibonacciFirstController> _logger;

    public FibonacciFirstController(IRepo repo, ITaskListener taskListener, IHttpClientService httpClientService, ILogger<FibonacciFirstController> logger)
    {
        _repo = repo;
        _taskListener = taskListener;
        _httpClientService = httpClientService;
        _logger = logger;
    }

    /// <summary>
    /// todo
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    [HttpGet("{number}")]
    public async Task<IActionResult> Get(int number)
    {
        if (_repo.HasNumber(number))
        {
            return Ok(_repo.GetAllFibNumber(number));
        }
        else
        {
            // _taskListener.StartListener(number); // todo в отдельный сервис
            var oneFibNumberWithPrev = _repo.GetMaxSaveNumber();
            var message = new MessageRequestFib(
                TaskNumber: number,
                Number: oneFibNumberWithPrev.Number + 1,
                Fib: oneFibNumberWithPrev.Fib,
                PrevFib: oneFibNumberWithPrev.PrevFib);
            var result = await _httpClientService.SendDataAsync(message, HttpContext.RequestAborted);
            if (result!= HttpStatusCode.OK)
            {
                return BadRequest("calculation is not possible. Service problem");
            }

            return Ok("Calculation has started. Check back later for results.");
        }
    }
}