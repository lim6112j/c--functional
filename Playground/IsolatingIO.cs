using static System.Console;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
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
    // abstracting IO with an Interface.
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
    }
    public class DefaultDateTimeService : IDateTimeService
        {
            public DateTime UtcNow => DateTime.UtcNow;
        }
    public class BicFormatValidator : IValidator<MakeTransfer>
    {
        static readonly Regex regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$");
        public bool IsValid(MakeTransfer transfer) => regex.IsMatch(transfer.Bic);
    }
    // avoiding trivial constructor
    public record DateNotPastValidatorStruct(IDateTimeService DateService) : IValidator<MakeTransfer> {
        private IDateTimeService DataService {get;}  = DateService;
        public bool IsValid(MakeTransfer request) => DataService.UtcNow.Date <= request.Date.Date;
    }
    public class DateNotPastValidator : IValidator<MakeTransfer>
    {
        private readonly IDateTimeService  dateService;
        public DateNotPastValidator(IDateTimeService dateService) {
            this.dateService = dateService;
        }
        public bool IsValid(MakeTransfer transfer) => (dateService.UtcNow.Date <= transfer.Date.Date);
    }
    public record DateNotPastValidatorValue(DateTime Today) : IValidator<MakeTransfer>{
        public bool IsValid(MakeTransfer transfer) => Today <= transfer.Date.Date;
    }
    public class IsolatingIO
    {
        public void ConfigureSerrvices(IServiceCollection services) {
            services.AddTransient<DateNotPastValidatorValue> (_ => new DateNotPastValidatorValue(DateTime.UtcNow.Date));
        }
        public static void Print()
        {
            WriteLine("hello");
        }
    }
}
