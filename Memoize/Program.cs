using System.Collections.Concurrent;
using System.Diagnostics;

namespace Memoize;
public class program {
  public static void Main() {
    Func<int, int>? fib = null;
    fib = n => n > 1 ? fib!(n-1) + fib(n-2) : n;
    var sw = Stopwatch.StartNew();
    for (int i=0; i<10000;i++)
      fib(10);
    Console.WriteLine(sw.ElapsedTicks);
    fib = fib.Memoize();
    sw = Stopwatch.StartNew();
    for (int i=0; i<10000;i++)
      fib(10);
    Console.WriteLine(sw.ElapsedTicks);
    Console.WriteLine(fib(10));
  }
}
public static class Memoizer
{
  public static Func<R> Memoize<R>(Func<R> func)
  {
    object? cache = null;
    return () =>
    {
      if (cache == null)
        cache = func();
      return (R)cache!;
    };
  }
  public static Func<T, R> Memoize<T, R>(Func<T, R> f) where T : notnull
  {
    var cache = new Dictionary<T, R>();
    return x =>
    {
      if (cache.TryGetValue(x, out R? value))
        return value;
      value = f(x);
      cache.Add(x, value);
      return value;
    };
  }
  public static Func<T, R> ThreadSafeMemoize<T, R>(Func<T,R> f) where T: notnull
  {
    var cache = new ConcurrentDictionary<T, R>();
    return argument => cache.GetOrAdd(argument, f);
  }
}
public static class MemoizationExtensions {
  public static Func<R> Memoize<R>(this Func<R> f)
  {
    return Memoizer.Memoize(f);
  }
  public static Func<T,R> Memoize<T, R>(this Func<T, R> f) where T: notnull
  {
    return Memoizer.Memoize(f);
  }
  public static Func<T, R> ThreadSafeMemoize<T, R>(this Func<T, R> f) where T: notnull
  {
    return Memoizer.ThreadSafeMemoize(f);
  }
}
