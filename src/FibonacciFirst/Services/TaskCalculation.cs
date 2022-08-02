namespace Fibonacci.Services;

public interface ITaskListener
{
    public bool IsCalculation { get; }
    public int NumberCalculation { get; set; }
    public void StartListener(int number);
    public void StopListener();
}