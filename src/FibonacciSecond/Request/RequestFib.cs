namespace FibonacciSecond.Request;

public record RequestFib(int Number, long First, long Second);

public enum ResponseError
{
    None,
    Overflow, // toto добавить
    WrongInput
}