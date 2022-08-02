using System.Net;
using System.Text;
using System.Text.Json;
using Common;
using Common.Contract;
using Fibonacci.Application.Interfaces;
using Fibonacci.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Fibonacci.Infrastructure;

public class HttpClientService : IHttpClientService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IOptions<HttpClientSettings> _options;
    private readonly ILogger<HttpClientService> _logger;

    public HttpClientService(IHttpClientFactory clientFactory, IOptions<HttpClientSettings> options,ILogger<HttpClientService> logger)
    {
        _clientFactory = clientFactory;
        _options = options;
        _logger = logger;

    }

    public async Task<HttpStatusCode> SendDataAsync(MessageRequestFib messageRequestFib, CancellationToken cancellationToken)
    {
        var client = _clientFactory.CreateClient();
        var request = new StringContent(JsonSerializer.Serialize(messageRequestFib), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(_options.Value.Url, request, cancellationToken);
        return response.StatusCode;
    }
}