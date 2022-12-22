using static System.Console;
using System.Text.RegularExpressions;
namespace Playground
{
    // OO design
    public abstract record Command(DateTime Timestamp);
    public record MakeTransfer
        (
            Guid DebitedAccountId,
            string Beneficiary,
            string Iban,
            string Bic,
            DateTime Date,
            decimal Amount,
            string Reference,
            DateTime Timestamp = default
        ) : Command(Timestamp)
    {
        internal static MakeTransfer Dummy
            => new(default, default, default, default, default, default, default);
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
    public class DateNotPastValidator : IValidator<MakeTransfer>
    {
        public bool IsValid(MakeTransfer transfer) => (DateTime.UtcNow.Date <= transfer.Date.Date);
    }
    public static class IsolatingIO
    {
        public static void Print()
        {
            WriteLine("hello");
        }
    }
}
