namespace Ch8;
using static System.Math;
using Unit = System.ValueTuple;
public class Ch8
{
    record Candidate ( int age );
    record Reason {}
    record Ingredients{}
    record Food {}

    // Either<L, R> = Left(L) | Right(R)
    public static void Main()
    {
        Either<string, double> Calc(double x, double y)
        {
            if (y == 0) return "y cannot be 0";
            if (x != 0 && Sign(x) != Sign(y))
                return "x / y can not be negative";
            return Sqrt(x / y);
        }
        var right = new Either.Right<int>(12);
        var either = new Either<string, int>(10);
        Console.WriteLine("either right value : " + either.ToString());
        Console.WriteLine("Hello, World!" + right.ToString());

        Console.WriteLine("calculation Calc(3, 0)" + Calc(3, 0).ToString());
        Console.WriteLine("calculation Calc(-3, 3)" + Calc(-3, 3).ToString());
        Console.WriteLine("calculation Calc(-3, -3)" + Calc(-3, -3).ToString());

        var result = either.Map(x => x * 2);
        Console.WriteLine("either map , x = 10, fn: x -> x * 2 => " + result);
        var result2 = either.Map(x => x - 9).Bind((int x) => new Either<string, int>(x)).ForEach(Console.WriteLine);

        Func<Candidate, bool> IsEligible;
        Func<Candidate, Option<Candidate>> TechTest;
        Func<Candidate, Option<Candidate>> Interview;
        Option<Candidate> Recruit(Candidate c)
          => new Some<Candidate>(c)
            .Where(IsEligible)
            .Bind(TechTest)
            .Bind(Interview);

        Func<Either<Reason, Unit>> WakeupEarly = () => { Console.WriteLine("woke up early"); return default;};
        Func<Either<Reason, Ingredients>> ShopForIngredients = () => new Either.Right<Ingredients>(new Ingredients());
        Func<Ingredients, Either<Reason, Food>> CookRecipe = ingredient => new Either.Right<Food>(new Food());
        Action<Food> EnjoyTogether = (food) => Console.WriteLine("enjoy food"); 
        Action<Reason> ComplainAbout = (reason) => Console.WriteLine("complaining");
        Action OrderPizza = () => Console.WriteLine("Ordered Pizza");
        WakeupEarly()
          .Bind(_ => ShopForIngredients())
          .Bind(CookRecipe)
          .Match<Unit>
          (
           Right: dish => {EnjoyTogether(dish); return default;},
           Left: reason =>
           {
              ComplainAbout(reason);
              OrderPizza();
              return default;
           }
           );
    }
}
