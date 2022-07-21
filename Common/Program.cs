﻿// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
using Unit = System.ValueTuple;
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
    public class Common
    {

        static Risk CalculateRiskProfile(Age age, Gender gender)
        {
            var threshold = (gender == Gender.Female) ? 62 : 60;
            return (age < threshold) ? Risk.Low : Risk.High;
        }
        internal static void Main()
        {
            Risk risk = CalculateRiskProfile(new Age(60), Gender.Female);
            Console.WriteLine("Hello, World! {0}", risk);

        }
    }
}
