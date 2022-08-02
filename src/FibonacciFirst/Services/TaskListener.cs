namespace Fibonacci.Services;

public class TaskListener : ITaskListener
{
    private bool _isCalculation;
    public bool IsCalculation => _isCalculation;
    public int NumberCalculation { get; set; }

    public void StartListener(int number)
    {
        if (NumberCalculation < number)
        {
            NumberCalculation = number;
        }

        _isCalculation = true;
    }

    public void StopListener()
    {
        _isCalculation = false;
    }
}