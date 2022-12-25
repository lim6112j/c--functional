static string Abbreviate(string s)
    => s.Substring(0, Math.Min(2, s.Length)).ToLower();
static string AbbreviateName(Person p)
    => Abbreviate(p.FirstName) + Abbreviate(p.LastName);
static string AppendDomain(string localPart)
    => $"{localPart}@manning.com";
Func<Person, string> emailFor =
    p => AppendDomain(AbbreviateName(p));
var joe = new Person("Joe", "blogger");
var email = emailFor(joe);
Console.WriteLine(joe + " : " + email);
record Person(string FirstName, string LastName);
