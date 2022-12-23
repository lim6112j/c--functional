// See https://aka.ms/new-console-template for more information
using LaYumba.Functional;
using static LaYumba.Functional.F;
using static System.Console;
public static class Int {
    public static Option<int> Parse(string s) => int.TryParse(s, out int result) ? Some(result) : None;
}
public class Partial {
    public static void Main() {
        var val = Int.Parse("10");
        var val2 = Int.Parse("hello");
        WriteLine("val : " + val);
        WriteLine("val2 : " + val2);
    }
}
