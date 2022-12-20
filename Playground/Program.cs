using System.Text;
// See https://aka.ms/new-console-template for more information
namespace Playground
{
    public static class Hof
    {
        /// <summary> linq count method </summary>
        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            int count = 0;
            foreach (TSource element in source)
            {
                checked
                {
                    if (predicate(element))
                    {
                        count++;
                    }
                }

            }
            return count;
        }
        /// <summary> compose functions </summary>
        public static Func<T, TReturn2> Compose<T, TReturn1, TReturn2>(this Func<TReturn1, TReturn2> func1, Func<T, TReturn1> func2) {
            return x => func1(func2(x));
        }
        /// <summary> string builder implementation </summary>
        public static StringBuilder AppendWhen(this StringBuilder sb, string value, bool predicate) => predicate ? sb.Append(value) : sb;
    }

    public class Playground
    {
        static bool dividedBy3(int num) {
            return num % 3 == 0;
        }
        public static void Main()
        {
            List<int> list = new List<int>{1,2,3, 4,5,6};
            Func<int, bool> pred = dividedBy3;
            Console.WriteLine("Linq count : " + Hof.Count(list, pred));

            Func<int, int> square = x => x*x;
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
        }
    }
}
