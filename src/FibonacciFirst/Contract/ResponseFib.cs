namespace Fibonacci.Contract;


public record OneFibNumberWithPrev(int Number, long Fib, long PrevFib);

public record OneFibNumber(int Number, long Fib);