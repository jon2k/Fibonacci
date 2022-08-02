﻿using System.Collections.Concurrent;
using Fibonacci.Contract;

namespace Fibonacci.Storage;

internal class Repo : IRepo
{
    // private readonly ConcurrentDictionary<int, long> _concurrentDictionary;
    private readonly List<OneFibNumber> _list; // todo concurent
    private readonly object _lock = new();

    public Repo()
    {
        _list = new List<OneFibNumber>();
        // _concurrentDictionary = new ConcurrentDictionary<int, long>();
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

    public OneFibNumberWithPrev GetMaxSaveNumber()
    {
        lock (_lock)
        {
            if (_list.Count == 0)
            {
                return new OneFibNumberWithPrev(
                    Number: 0,
                    Fib: 0,
                    PrevFib: 0);
            }

            if (_list.Count == 1)
            {
                return new OneFibNumberWithPrev(
                    Number: 1,
                    Fib: 1,
                    PrevFib: 0);
            }

            return new OneFibNumberWithPrev(
                Number: _list[_list.Count - 1].Number,
                Fib: _list[_list.Count - 1].Fib,
                PrevFib: _list[_list.Count - 2].Fib);
        }
    }
}