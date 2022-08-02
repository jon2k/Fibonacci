using Fibonacci.Contract;

namespace Fibonacci.Storage;

public interface IRepo
{
    List<long> GetAllFibNumber(int number);
    void AddFibNumber(int number, long result);
    bool HasNumber(int number);
    OneFibNumberWithPrev GetMaxSaveNumber();
}