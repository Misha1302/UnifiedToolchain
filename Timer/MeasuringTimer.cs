using System.Diagnostics;

namespace Timer;

public class MeasuringTimer
{
    public static readonly MeasuringTimer Instance = new();

    public long Measure(Action action)
    {
        var sw = Stopwatch.StartNew();
        action();
        return sw.ElapsedMilliseconds;
    }
}