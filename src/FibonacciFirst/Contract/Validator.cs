namespace Fibonacci.Contract;
using FluentValidation;

public class RequestValidator : AbstractValidator<int> 
{
    public  RequestValidator() 
    {
        RuleFor(x => x).LessThan(30); // todo не работает
    }
}