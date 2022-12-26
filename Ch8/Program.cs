// See https://aka.ms/new-console-template for more information
namespace Ch8;
using LaYumba.Functional;
using static LaYumba.Functional.F;
public class Ch8
{
    // Either<L, R> = Left(L) | Right(R)
    public static void Main()
    {
        var right = Right(12);
        Console.WriteLine("Hello, World!"  + right);
    }
}
