namespace FunctionalComposition;
public class Program
{
    private static void Main(string[] args)
    {
        var joe = new Person("Joe", "blogger");
        var email = joe.AbbreviateName().AppendDomain();
        Console.WriteLine(joe + " : " + email);
    }

}

record Person(string FirstName, string LastName);
static class FuncComposing
{
    internal static string Abbreviate(string s)
        => s.Substring(0, Math.Min(2, s.Length)).ToLower();
    internal static string AbbreviateName(this Person p)
        => Abbreviate(p.FirstName) + Abbreviate(p.LastName);
    internal static string AppendDomain(this string localPart)
        => $"{localPart}@manning.com";
}
