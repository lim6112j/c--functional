// See https://aka.ms/new-console-template for more information
//
using System.Diagnostics;
Console.WriteLine("hello");
public static class Instrumentation
{
    public static T Time<T>(string op, Func<T> f)
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        T t = f();
        stopWatch.Stop();
        Console.WriteLine($"{op} took {stopWatch.ElapsedMilliseconds}ms");
        return t;
    }
    public static void Time(string op, Action act)
    {
        var sw = new Stopwatch();
        sw.Start();
        act();
        sw.Stop();
        Console.WriteLine($"{op} took {sw.ElapsedMilliseconds}ms");
    }
}
