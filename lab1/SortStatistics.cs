namespace lab1;

public class SortStatistics
{
    public int Comparisons { get; set; }
    public int Swaps { get; set; }
    public int RecursiveCalls { get; set; }
    public TimeSpan ExecutionTime { get; set; }

    public void Reset()
    {
        Comparisons = 0;
        Swaps = 0;
        RecursiveCalls = 0;
        ExecutionTime = TimeSpan.Zero;
    }
}