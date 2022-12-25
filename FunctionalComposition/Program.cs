namespace FunctionalComposition;
public class Program
{
    private static void Main(string[] args)
    {
        var joe = new Person("Joe", "blogger");
        var email = joe.AbbreviateName().AppendDomain();
        Console.WriteLine(joe + " : " + email);
        Circle c1 = new Circle(new Point(10, 10), 10);
        Circle c2 = c1.Modify();
        Console.WriteLine(c1);
        Console.WriteLine(c2);
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
record Circle(Point Center, double Radius);
record Point(double X, double Y);
static class Geometry
{
    internal static Circle Move(this Circle c, double x, double y)
        => new Circle(new Point(c.Center.X + x, c.Center.Y + y), c.Radius);
    internal static Circle Scale(this Circle c, double factor)
        => new Circle(c.Center, c.Radius * factor);
    internal static Circle Modify(this Circle c)
        => c
        .Move(10, 10)
        .Scale(0.5);
}
