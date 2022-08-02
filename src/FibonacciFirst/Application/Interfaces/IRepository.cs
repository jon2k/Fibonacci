using Fibonacci.Contract;

namespace Fibonacci.Application.Interfaces;

internal interface IRepository
{
    List<long> GetAllFibNumber(int number);
    void AddFibNumber(int number, long result);
    bool HasNumber(int number);
    OneFibNumberWithPrev GetMaxSaveNumber();
}