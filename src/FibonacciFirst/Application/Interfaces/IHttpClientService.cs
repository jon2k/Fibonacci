using System.Net;
using Common;
using Common.Contract;

namespace Fibonacci.Application.Interfaces;

public interface IHttpClientService
{
    public Task<HttpStatusCode> SendDataAsync(MessageRequestFib message, CancellationToken cancellationToken);
}