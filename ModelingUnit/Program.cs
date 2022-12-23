// See https://aka.ms/new-console-template for more information
//
using Unit = System.ValueTuple;
using System.Diagnostics;
Console.WriteLine("hello");
public static class ActionExt
{
    /// <summary> Actions to Unit-returning  Func </summary>
    public static Func<Unit> ToFunc(this Action action) => () => { action(); return default; };
    /// <summary> Actions to Unit-returning  Func
    public static Func<T, Unit> ToFunc<T>(this Action<T> action) => (t) => { action(t); return default; };
}
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
        => Time<Unit>(op, act.ToFunc());
}
