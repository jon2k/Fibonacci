using Fibonacci.Application.Interfaces;
using Fibonacci.Contract;

namespace Fibonacci.Infrastructure;

internal class Repository : IRepository
{
    private readonly List<OneFibNumber> _list;
    private readonly object _lock = new();

    public Repository()
    {
        _list = new List<OneFibNumber>
        {
            new OneFibNumber(1, 0),
            new OneFibNumber(2, 1)
        };
    }

    public List<long> GetAllFibNumber(int number)
    {
        lock (_lock)
        {
            return _list.Where(n => n.Number <= number)
                .Select(n => n.Fib)
                .ToList();
        }
    }

    public void AddFibNumber(int number, long result)
    {
        lock (_lock)
        {
            _list.Add(new OneFibNumber(number, result));
        }
    }

    public bool HasNumber(int number)
    {
        lock (_lock)
        {
            var result = _list.FirstOrDefault(n => n.Number == number);
        
            return result != null;
        }
    }

    public OneFibNumber? GetLastFibNumber()
    {
        lock (_lock)
        {
            return _list.MaxBy(n => n.Number);
        }
    }

    public OneFibNumberWithPrev GetMaxSaveNumber()
    {
        lock (_lock)
        {
            return new OneFibNumberWithPrev(
                Number: _list[_list.Count - 1].Number,
                Fib: _list[_list.Count - 1].Fib,
                PrevFib: _list[_list.Count - 2].Fib);
        }
    }
}