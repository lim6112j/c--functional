using System.Text;
namespace Playground;
public static class Hof
{
    /// <summary> linq count method </summary>
    public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        int count = 0;
        foreach (TSource element in source)
        {
            checked
            {
                if (predicate(element))
                {
                    count++;
                }
            }

        }
        return count;
    }
    /// <summary> compose functions </summary>
    public static Func<T, TReturn2> Compose<T, TReturn1, TReturn2>(this Func<TReturn1, TReturn2> func1, Func<T, TReturn1> func2)
    {
        return x => func1(func2(x));
    }
    /// <summary> string builder implementation </summary>
    public static StringBuilder AppendWhen(this StringBuilder sb, string value, bool predicate) => predicate ? sb.Append(value) : sb;
    /// <summary> generic Where custom function </summary>
    public static IEnumerable<T> WhereCustom<T>(this IEnumerable<T> ts, Func<T, bool> predicate)
    {
        foreach (T t in ts)
        {
            if (predicate(t))
                yield return t;
        }
    }
    /// <summary> swapargs </summary>
    public static Func<T2, T1, R> SwapArgs<T1, T2, R>(this Func<T1, T2, R> f) => (x, y) => f(y, x);
}
