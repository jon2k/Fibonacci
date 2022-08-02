namespace Common;


public record MessageResponseFib(int TaskNumber, int CurrentNumber, long Sum);
public record MessageRequestFib(int TaskNumber, int Number, long Fib, long PrevFib);