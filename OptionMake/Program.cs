public struct NoneType { }
public abstract record Option<T>
{
    public static implicit operator Option<T>(NoneType _) => new None<T>();
}
public record None<T> : Option<T>;
public record Some<T>(T value) : Option<T>;
public static class OptionExt
{
    public static R Match<T, R>(this Option<T> opt, Func<R> None, Func<T, R> Some) => opt switch
    {
        None<T> => None(),
        Some<T>(var t) => Some(t),
        _ => throw new ArgumentException("option must be none or some")
    };
}
public class OptionMake
{
    public static readonly NoneType None = default;
    public static string Greet(Option<string> greetee) => greetee.Match
    (
        None: () => "sorry, wwho?",
        Some: (name) => $"hello, {name}"
    );
    public static void Main()
    {
        var greet = Greet(None);
        Console.WriteLine(greet);
    }
}
