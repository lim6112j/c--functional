namespace Playground;
static class StringExt
{
    public static string ToSentenceCase(this string s) // pure function
        => s == string.Empty
        ? string.Empty
        : char.ToUpperInvariant(s[0]) + s.ToLower()[1..];
}
