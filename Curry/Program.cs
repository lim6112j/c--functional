// See https://aka.ms/new-console-template for more information
using System.Text;

namespace Curry;
static class extension {
  // extension func should be in non-generic static class
    public static Func<T1, Func<T2, R>> CurryFunc<T1, T2, R> (this Func<T1, T2, R> f) => t1 => t2 => f(t1, t2);
    public static Func<T, R2> Compose<T, R1, R2>(this Func<R1, R2> f1, Func<T, R1> f2) {
      return x => f1(f2(x));
    }
    public static StringBuilder AppendWhen(this StringBuilder sb, string value, bool predicate) => predicate ? sb.Append(value) : sb;
}
public class Program {
  // delegate
    static Func<String, String, String> greet = (String greeting, String name) => $"{greeting}, {name}";
    private static void Main() {
        var curriedGreet = greet.CurryFunc();
        Console.WriteLine(greet("Hello", "Ciel"));
        Console.WriteLine(curriedGreet("Hello")("Ciel"));
        // extension method : method chaining
        Func<int, int> square = (x) => x * x;
        Func<int, int> negate = x => x + -1;
        Func<int, string> toString = s => s.ToString();
        Func<int, string> squareNegateToString = toString.Compose(negate).Compose(square);
        Console.WriteLine(squareNegateToString(2));
        // extension method : add new behavior to existing class
        string htmlButton = new StringBuilder().Append("<button").AppendWhen(" disabled", true).Append(">Click me</button>").ToString();
        Console.WriteLine(htmlButton);
    }
}
