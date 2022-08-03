using FluentValidation;

namespace Fibonacci.Contract.Validations;

public class NumberValidator : AbstractValidator<int> 
{
    public NumberValidator() 
    {
        RuleFor(x => x).LessThan(93);
        RuleFor(x => x).GreaterThan(0);
    }
}