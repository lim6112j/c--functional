// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;
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
    public record DateNotPastValidator(IDateTimeService DateService) : IValidator<MakeTransfer>
    {
        private IDateTimeService DateService { get; } = DateService;
        public bool IsValid(MakeTransfer transfer) => DateService.UtcNow <= transfer.Date.Date;
    }
    public class Common
    {
        internal static void Main()
        {
            Console.WriteLine("Hello, World!");

        }
    }
}
