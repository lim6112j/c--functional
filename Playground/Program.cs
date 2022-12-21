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
        /// <summary> generic Where custom function </summary>
        public static IEnumerable<T> WhereCustom<T>(this IEnumerable<T> ts, Func<T, bool> predicate) {
            foreach(T t in ts) {
                if(predicate(t))
                    yield return t;
            }
        }
    }
    public class Playground
    {
        private static bool dividedBy3(int num) {
            return num % 3 == 0;
        }
        private static IEnumerable<int> GreaterThan(int[] a, int v)
        {
            foreach(int n in a) {
                if (n > v) yield return n;
            }
        }
        public static void Main()
        {
            List<int> list = new List<int>{1,2,3, 4,5,6};
            Func<int, bool> pred = dividedBy3;
            Console.WriteLine("Linq count : " + list.Count(pred));

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

            // yield IEnumerable
            int [] a = { 1,2,3,4,5,6,7,8,9 };
            foreach(int n in GreaterThan(a, 3)) {
                Console.WriteLine("{1,2,3,4,5} greater than 3 : " + n);
            }
            foreach(int n in a.WhereCustom(x => x % 3 == 0)) {
                Console.WriteLine("HOF Where function : " + n);
            }
        }

    }
}
