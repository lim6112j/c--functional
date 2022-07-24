// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using Unit = System.ValueTuple;
using LaYumba.Functional;
using static LaYumba.Functional.F;
using String = LaYumba.Functional.String;
using ExtensionMethods;
using static System.Console;

namespace ExtensionMethods
{
    using Common;
    public static class MyExtensions
    {
        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries
            ).Count();
        }
        public static AccountState Activate(this AccountState original) => original with
        {
            Status = AccountStatus.Active
        };
        public static AccountState RedFlag(this AccountState original) => original with
        {
            Status = AccountStatus.Frozen,
            AllowedOverdraft = 0m
        };
    }
}
namespace Common
{
    public abstract record Command(DateTime Timestamp);
    public record MakeTransfer(
        string Beneficiary,
        string Iban,
        string Bic,
        DateTime Date,
        decimal Amount,
        string Reference,
        DateTime Timestamp = default
    ) : Command(Timestamp)
    {
        public static MakeTransfer Dummy => new(default!, default!, default!, default!, default!, default!, default!);
    }
    public interface IValidator<T>
    {
        bool IsValid(T t);
    }
    public class BicFormatValidator : IValidator<MakeTransfer>
    {
        static readonly Regex regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$");

        public bool IsValid(MakeTransfer transfer) => regex.IsMatch(transfer.Bic);
    }
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
    }
    public class DefaultDateTimeService : IDateTimeService
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
    public delegate DateTime Clock();
    public record DateNotPastValidator(Clock Clock) : IValidator<MakeTransfer>
    {
        public bool IsValid(MakeTransfer transfer) => Clock().Date <= transfer.Date.Date;
    }
    enum Risk { Low, Medium, High }
    enum Gender { Female, Male }
    public struct Age
    {
        private int Value { get; }
        public Age(int value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException($"{value} is not a valid age");
            }
            Value = value;
        }
        private static bool IsValid(int age) => age > 0 && age < 120;
        public static bool operator <(Age l, Age r)
            => l.Value < r.Value;
        public static bool operator >(Age l, Age r)
            => l.Value > r.Value;
        public static bool operator <(Age l, int r)
            => l < new Age(r);
        public static bool operator >(Age l, int r)
            => l > new Age(r);
    }
    readonly record struct HealthData
    {
        readonly Age age;
        readonly Gender gender;
    }
    public static class ActionExt
    {
        public static Func<Unit> ToFunc(this Action action) => () => { action(); return default; };
        public static Func<T, Unit> ToFunc<T>(this Action<T> action) => (t) => { action(t); return default; };
    }
    public record PhoneNumber
    {
        public NumberType Type { get; }
        public CountryCode Country { get; }
        public Number Nr { get; }
        public enum NumberType { Mobile, Home, Office }
        public struct Number {/*....*/}
        public static Func<NumberType, CountryCode, Number, PhoneNumber> Create = (type, country, number) => new(type, country, number);
        PhoneNumber(NumberType type, CountryCode country, Number number)
        {
            Type = type;
            Country = country;
            Nr = number;
        }
    }
    public class CountryCode {/*...*/}

    // immutable state
    public enum AccountStatus
    {
        Requested, Active, Frozen, Dormant, Closed
    }
    public record AccountState
    (
        CurrencyCode Currency,
        AccountStatus Status = AccountStatus.Requested,
        decimal AllowedOverdraft = 0m,
        IEnumerable<Transaction>? TransactionHistory = null
    );
    public record CurrencyCode(string Value)
    {
        public static implicit operator string(CurrencyCode c) => c.Value;
        public static implicit operator CurrencyCode(string currencyStr) => new(currencyStr);
    };

    public record Transaction
    (
        decimal Amount,
        string Description,
        DateTime Date
    );
    public class Common
    {
        string Greet(Option<string> greetee)
            => greetee.Match(
                None: () => "sorry, who??",
                Some: (name) => $"Hello,  {name}"
            );
        static Risk CalculateRiskProfile(Age age, Gender gender)
        {
            var threshold = (gender == Gender.Female) ? 62 : 60;
            return (age < threshold) ? Risk.Low : Risk.High;
        }
        record Person(string FirstName, string LastName);
        static string AbbreviateName(Person p)
            => Abbreviate(p.FirstName) + Abbreviate(p.LastName);
        static string Abbreviate(string s)
            => s.Substring(0, Math.Min(2, s.Length)).ToLower();
        static string AppendDomain(string localport)
            => $"{localport}@manning.com";
        static Func<Person, string> emailFor =
            p => AppendDomain(AbbreviateName(p));

        internal static void Main()
        {
            var common = new Common();
            Console.WriteLine("{0}", common.Greet(Some("Jane")));
            Risk risk = CalculateRiskProfile(new Age(60), Gender.Female);
            Console.WriteLine("Hello, World! {0}", risk);
            Option<string> name = Some("enrico");
            name
            .Map(String.ToUpper)
            .ForEach(Console.WriteLine);
            IEnumerable<string> names = new[] { "Constance", "Albert" };
            names
                .Map(String.ToUpper)
                .ForEach(Console.WriteLine);
            var joe = new Person("Joe", "Bloggs");
            var email = emailFor(joe);
            Console.WriteLine("email for joe : {0}", email);
            string s = "Hello Extension methods";
            Console.WriteLine("extension methods word Count = {0} ", s.WordCount());
            WriteLine("Enter first append");
            var s1 = ReadLine();
            WriteLine("Enter first append");
            var s2 = ReadLine();
            var result = from a in Int.Parse(s1)
                         from b in Int.Parse(s2)
                         select a + b;
            WriteLine(result.Match(
                          None: () => "Please enter 2 valid integers",
                          Some: (r) => $"{s1} + {s2} = {r}"
                      ));
            var result2 = Some(new Func<int, int, int>((a, b) => a + b))
                .Apply(Int.Parse(s1))
                .Apply(Int.Parse(s2));

            WriteLine(result2.Match(
                          None: () => "Please enter 2 valid integers",
                          Some: (r) => $"{s1} + {s2} = {r}"
                      ));
            var result3 = Int.Parse(s1)
                .Bind(a => Int.Parse(s2).Map(b => a + b));

            WriteLine(result2.Match(
                          None: () => "Please enter 2 valid integers",
                          Some: (r) => $"{s1} + {s2} = {r}"
                      ));
            var original = new AccountState(Currency: "EUR");
            var activated = original.Activate();
            WriteLine("Original Status : {0}", original.Status);
            WriteLine("Original Currency : {0}", original.Currency);
            WriteLine("Activated Status : {0}", activated.Status);
            WriteLine("Activated Currency : {0}", activated.Currency);

            var redflag = original.RedFlag();

            WriteLine("Redflag Status : {0}", redflag.Status);
            WriteLine("Redflag Currency : {0}", redflag.Currency);
        }

    }
}
