// See https://aka.ms/new-console-template for more information
using LaYumba.Functional;
using System.Collections.Specialized;
using static LaYumba.Functional.F;
using static System.Console;
/// <summary> partial to total function with option</summary>
public static class Int {
    public static Option<int> Parse(string s) => int.TryParse(s, out int result) ? Some(result) : None;
}

public class Partial {
    /// lookup : (NameValueCollection, string) -> Option<string>
    public static Option<string?> Lookup(NameValueCollection col, string key) => col[key] == null ? None: Some(col[key]);
    /// lookup : (IDictionary<K, T> , K) -> Option<T>
    public static Option<T> Lookup<K, T> (IDictionary<K, T> dict, K key) => dict.TryGetValue(key, out T? value) ? Some(value) : None;
    public static void Main() {
        var val = Int.Parse("10");
        var val2 = Int.Parse("hello");
        WriteLine("val : " + val);
        WriteLine("val2 : " + val2);
        var col = new NameValueCollection();
        col["green"] = "green";
        WriteLine(Lookup(col, "green"));
        WriteLine(Lookup(col, "blue"));
        var dict = new Dictionary<string, string>().Lookup("green");
        WriteLine("dictionary value : null => " + dict);
    }
}
