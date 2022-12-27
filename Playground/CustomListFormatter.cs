using P = System.Linq.ParallelEnumerable;
namespace Playground;
class ListFormatter
{
    int counter;
    string PrependCounter(string s) => $"{++counter}. {s}"; // impure
    public List<string> Format(List<string> list)
        => list
        .Select(StringExt.ToSentenceCase) // pure
        .Select(PrependCounter) //impure
        .ToList();
}
class ListFormatterParallel
{
    int counter;
    string PrependCounter(string s) => $"{++counter}. {s}";
    public List<string> Format(List<string> list)
        => list
        .AsParallel()
        .Select(StringExt.ToSentenceCase)
        .Select(PrependCounter)
        .ToList();

}
static class ListFormatterStatic
{
    public static List<string> Format(List<string> list)
        => list
        .AsParallel()
        .Select(StringExt.ToSentenceCase)
        .Zip(P.Range(1, list.Count), (s, i) => $"{i}. {s}")
        .ToList();
}
