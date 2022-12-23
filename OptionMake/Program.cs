using System.Collections.Specialized;

struct NoneType { }
abstract record Option<T>
{
    public static implicit operator Option<T>(NoneType _) => new None<T>();
    public static implicit operator Option<T>(T value) => value == null ? new None<T>() : new Some<T>(value);
}
record None<T> : Option<T>;
record Some<T> : Option<T> {
    private T Value {get;}
    public Some(T value)
        => Value = value ?? throw new ArgumentNullException();
    public void Deconstruct(out T value)
        => value = Value;
}
static class OptionExt
{
    public static R Match<T, R>(this Option<T> opt, Func<R> None, Func<T, R> Some) => opt switch
    {
        None<T> => None(),
        Some<T>(var t) => Some(t),
        _ => throw new ArgumentException("option must be none or some")
    };
}
class OptionMake
{
    public static Option<T> Some<T>(T t) => new Some<T>(t);
    static readonly NoneType None = default;
    static string Greet(Option<string> greetee) => greetee.Match
   (
       () => "sorry, wwho?",
       (name) => $"hello, {name}"
   );
    public static void Main()
    {
        var greet = Greet(None);
        Console.WriteLine(greet);
        var greet2 = Greet(Some("John"));
        Console.WriteLine(greet2);
        Console.WriteLine(Greet(None));
        Console.WriteLine(Greet("John"));
        var empty = new NameValueCollection();
        Option<string> green = empty["green"];
        Console.WriteLine(Greet(green));
    }
}
