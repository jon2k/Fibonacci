using EasyNetQ;

namespace Common.Contract;


[Queue("Fibonacci", ExchangeName = "Fibonacci")]
public record MessageResponseFib(int TaskNumber, int CurrentNumber, long Sum);