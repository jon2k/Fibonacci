using System.Net;
using Common;
using Fibonacci.Contract;

namespace Fibonacci.Services;

public interface IHttpClientService
{
    public Task<HttpStatusCode> SendDataAsync(MessageRequestFib message, CancellationToken cancellationToken);
}