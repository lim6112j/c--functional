// See https://aka.ms/new-console-template for more information
namespace Curry;
static class extension {
    public static Func<T1, Func<T2, R>> CurryFunc<T1, T2, R> (this Func<T1, T2, R> f) => t1 => t2 => f(t1, t2);
}
public class Program {
    static Func<String, String, String> greet = (String greeting, String name) => $"{greeting}, {name}";
    private static void Main() {
        var curried = greet.CurryFunc();
        Console.WriteLine(greet("hello", "lim"));
        Console.WriteLine(curried("hello")("lim"));
    }
}
