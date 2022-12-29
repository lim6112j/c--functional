struct NoneType { }
abstract record Option<T>
{
    public static implicit operator Option<T>(NoneType _) => new None<T>();
    public static implicit operator Option<T>(T value) => value == null ? new None<T>() : new Some<T>(value);
}
record None<T> : Option<T>;
record Some<T> : Option<T>
{
    private T Value { get; }
    public Some(T value)
        => Value = value ?? throw new ArgumentNullException();
    public void Deconstruct(out T value)
        => value = Value;
}
static class OptionExt
{
    public static Option<R> Bind<T, R>
      (this Option<T> optT, Func<T, Option<R>> f)
      => optT.Match(
          () => None,
          t => f(t)
          );
    public static Option<T> Where<T>
      (this Option<T> optT, Func<T, bool> predicate)
      => optT.Match(
          () => None,
          (t) => predicate(t) ? optT : None);
    public static R Match<T, R>(this Option<T> opt, Func<R> None, Func<T, R> Some) => opt switch
    {
        None<T> => None(),
        Some<T>(var t) => Some(t),
        _ => throw new ArgumentException("option must be none or some")
    };
    public static Option<T> Some<T>(T t) => new Some<T>(t);
    static readonly NoneType None = default;
}
