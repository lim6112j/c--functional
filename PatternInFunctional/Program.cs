//
using static System.Linq.Enumerable;
using LaYumba.Functional;
var triple = (int x) => x * 3;
Console.WriteLine(Range(1, 3).Map(triple));
public static class PatternInFunctional
{
    static IEnumerable<R> Map<T, R>(this IEnumerable<T> ts, Func<T, R> f)
    {
        foreach (var t in ts)
            yield return f(t);
    }
}
