using Fibonacci.Application.Interfaces;
using MediatR;

namespace Fibonacci.Application.Query;

internal sealed class GetFibNumbersQueryHandler : IRequestHandler<GetFibNumbersQuery, List<long>>
{
    private readonly IRepository _repository;

    public GetFibNumbersQueryHandler(IRepository repository)
    {
        _repository = repository;
    }
    public Task<List<long>> Handle(GetFibNumbersQuery request, CancellationToken cancellationToken)
    {
        if (_repository.HasNumber(request.Number))
        {
            return Task.FromResult(_repository.GetAllFibNumber(request.Number));
        }

        return Task.FromResult(new List<long>());
    }
}