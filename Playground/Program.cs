using System.Text;
using static System.Console;
using static System.Linq.Enumerable;
// See https://aka.ms/new-console-template for more information
namespace Playground
{
    public class Playground
    {
        private static Func<int, bool> dividedBy(int num)
        {
            return (n) => n % num == 0;
        }
        private static IEnumerable<int> GreaterThan(int[] a, int v)
        {
            foreach (int n in a)
            {
                if (n > v) yield return n;
            }
        }
        public static void Main()
        {
            List<int> list = new List<int> { 1, 2, 3, 4, 5, 6 };
            Func<int, bool> pred = dividedBy(3);
            Console.WriteLine("Linq count : " + list.Count(pred));

            Func<int, int> square = x => x * x;
            Func<int, int> negate = x => x * -1;
            Func<int, string> toString = s => s.ToString();
            Func<int, string> squareNegateThenToString = toString.Compose(negate).Compose(square);
            Console.WriteLine("function composing 2 * -1 toString : " + squareNegateThenToString(2));


            var str = new StringBuilder()
                .Append("Hello ")
                .Append("World ")
                .ToString()
                .TrimEnd()
                .ToUpper();
            Console.WriteLine("stringbuilder using : " + str);

            var htmlButton = new StringBuilder()
                .Append("<Button")
                .AppendWhen(" disabled", true)
                .Append(">Click me</Button>")
                .ToString();
            Console.WriteLine("added AppendWhen to StringBuilder : " + htmlButton);

            // yield IEnumerable
            int[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            foreach (int n in GreaterThan(a, 3))
            {
                Console.WriteLine("{1,2,3,4,5} greater than 3 : " + n);
            }
            foreach (int n in a.WhereCustom(x => x % 3 == 0))
            {
                Console.WriteLine("HOF Where function : " + n);
            }

            // swap args
            Func<int, int, int> divideBy = (x, y) => x / y;
            var divideBySwapped = divideBy.SwapArgs();
            Console.WriteLine("swapArgs func : x/y to y/x ,5/10 to  10/5 : " + divideBySwapped(5, 10));


            // function factories
            Func<int, bool> isMod(int n) => i => i % n == 0;
            Console.WriteLine(" function fatories : ");
            Range(1, 20)
                .Where(isMod(7))
                .ToList()
                .ForEach(WriteLine);
            // side effects
            // avoiding state mutation
            var shoppingList = new List<string>{
                "coffee beans",
                "BANANAS",
                "Dates"
            };
            var watch = System.Diagnostics.Stopwatch.StartNew();
            WriteLine("avoiding state mutation");
            new ListFormatter()
                .Format(shoppingList)
                .ForEach(WriteLine);

            watch.Stop();
            WriteLine("elapsed time : " + watch.ElapsedMilliseconds);
            // pure function parallels well
            watch.Reset();
            watch.Start();
            WriteLine("avoiding state mutation: impure function applied in parallel");
            new ListFormatterParallel()
                .Format(shoppingList)
                .ForEach(WriteLine);
            watch.Stop();
            WriteLine("elapsed time : " + watch.ElapsedMilliseconds);
            // avoid using shared state
            watch.Reset();
            watch.Start();
            WriteLine("avoiding state mutation: avoidng shared state");
            ListFormatterStatic
                .Format(shoppingList)
                .ForEach(WriteLine);
            watch.Stop();
            WriteLine("elapsed time : " + watch.ElapsedMilliseconds);
            IsolatingIO.Print();
        }

    }
}
