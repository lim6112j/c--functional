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
            Console.WriteLine(Hof.Count(list, pred));
        }
    }
}
