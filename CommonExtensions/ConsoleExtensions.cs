namespace CommonExtensions;

public static class ConsoleExtensions
{
    public static void Print<T>(this T value, string format = "{0}")
    {
        Console.WriteLine(format, value);
    }
}