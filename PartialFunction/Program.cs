// See https://aka.ms/new-console-template for more information
using System.Collections.Specialized;
using static System.Console;
using static OptionStatic;
using static Age;
using Unit = System.ValueTuple;
public struct NoneType { }
public static class OptionStatic
{
    public static NoneType None => default;
    public static Option<T> Some<T>(T value) => new Option<T>(value);
}
public struct Option<T>
{
    readonly T? value;
    readonly bool isSome;

    public Option(T? value)
    {
        this.value = value;
        this.isSome = true;
    }
    public static implicit operator Option<T>(NoneType _) => default;
    public static implicit operator Option<T>(T value) => value == null ? None : Some(value);
    public R Match<R>(Func<R> None, Func<T, R> Some)
        => isSome ? Some(value!) : None();
}
/// <summary> partial to total function with option</summary>
public static class Int
{
    public static Option<int> Parse(string s) => int.TryParse(s, out int result) ? Some(result) : None;
}
public struct Age
{
    private int Value { get; }
    public static Option<Age> Create(int age) => IsValid(new Age(age).Value) ? Some(new Age(age)) : None;
    private Age(int value) => Value = value;
    private static bool IsValid(int age) => 0 <= age && age < 120;
    public static void Print(string sp, Option<Age> age) => age.Match<Func<Unit>>(
        None: () => { WriteLine(sp + "invalid"); return default; },
        Some: (value) => { WriteLine(sp + value.Value); return default; }
        );
}
public class Partial
{
    /// lookup : (NameValueCollection, string) -> Option<string>
    public static Option<string?> Lookup(NameValueCollection col, string key) => col[key] == null ? None : Some(col[key]);
    /// lookup : (IDictionary<K, T> , K) -> Option<T>
    public static Option<T> Lookup<K, T>(IDictionary<K, T> dict, K key) => dict.TryGetValue(key, out T? value) ? Some(value) : None;
    public static void Main()
    {
        var val = Int.Parse("10");
        var val2 = Int.Parse("hello");
        WriteLine("val : " + val.Match<string>(None: () => "invalid", Some: (value) => value.ToString()));
        WriteLine("val2 : " + val2.Match<string>(None: () => "invalid", Some: (value) => value.ToString()));
        var col = new NameValueCollection();
        col["green"] = "green";
        WriteLine(Lookup(col, "green").Match(
                      None: () => "who?",
                      Some: (value) => value
                  ));
        WriteLine(Lookup(col, "blue").Match(
                      None: () => "who?",
                      Some: (value) => value
                  ));
        Print("Age smart constructor : ", Create(130));
        Print("Age smart constructor : ", Create(100));
    }

}
