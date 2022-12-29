#nullable disable
namespace Ch8;
using Unit = System.ValueTuple;
public struct Either<L, R>
{
    internal L Left { get; }
    internal R Right { get; }
    private bool IsRight { get; }
    private bool IsLeft => !IsRight;
    internal Either(L left)
    {
        IsRight = false;
        Left = left;
        Right = default(R);
    }
    internal Either(R right)
    {
        IsRight = true;
        Right = right;
        Left = default(L);
    }
    public static implicit operator Either<L, R>(L left) => new Either<L, R>(left);
    public static implicit operator Either<L, R>(R right) => new Either<L, R>(right);
    public static implicit operator Either<L, R>(Either.Left<L> left) => new Either<L, R>(left.Value);
    public static implicit operator Either<L, R>(Either.Right<R> right) => new Either<L, R>(right.Value);
    public TR Match<TR>(Func<L, TR> Left, Func<R, TR> Right)
      => IsLeft ? Left(this.Left) : Right(this.Right);
    public override string ToString() => Match(l => $"Left: {l}", r => $"Right: {r}");
}
public static class Either
{
    public struct Left<L>
    {
        internal L Value { get; }
        internal Left(L value) { Value = value; }
        public override string ToString() => $"Left({Value})";
    }
    public struct Right<R>
    {
        internal R Value { get; }
        internal Right(R value) { Value = value; }
        public override string ToString() => $"Right({Value})";
    }
}
public static class ActionFunc
{
    public static Func<Unit> ToFunc(this Action action)
      => () => { action(); return default; };
    public static Func<T, Unit> ToFunc<T>(this Action<T> action)
      => (T t) => { action(t); return default; };
}
public static class EitherExt
{
    public static Either.Left<L> Left<L>(L l) => new Either.Left<L>(l);
    public static Either.Right<R> Right<R>(R r) => new Either.Right<R>(r);
    public static Either<L, RR> Map<L, R, RR>
      (
       this Either<L, R> @this,
       Func<R, RR> f
       )
      => @this.Match<Either<L, RR>>(
          l => Left(l),
          r => Right(f(r))
          );
    public static Either<L, Unit> ForEach<L, R>
      (this Either<L, R> either, Action<R> action)
      => Map<L, R, Unit>(either, action.ToFunc());
    public static Either<L, RR> Bind<L, R, RR>
      (
       this Either<L, R> either,
       Func<R, Either<L, RR>> f
       )
      => either.Match(
          l => Left(l),
          r => f(r)
          );
}
