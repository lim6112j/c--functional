
namespace CommonTests;
using Common;
public class Tests
{
    static DateTime presentDate = new DateTime(2025, 3, 12);
    private class FakeDateTimeService : IDateTimeService
    {
        public DateTime UtcNow => presentDate;
    }
    [Test]
    public void WnenTransferDateIsFuture_ThenValidationPasses()
    {
        // ikmpure test
        var ddts = new DefaultDateTimeService();
        var sut = new DateNotPastValidator(ddts);
        var transfer = MakeTransfer.Dummy with
        {
            Date = new DateTime(2025, 3, 12)
        };
        var actual = sut.IsValid(transfer);
        Assert.That(actual, Is.EqualTo(true));
    }
    [Test]
    public void WhenTransferDateIsFuture_ThenValidationPassesWithFakeData()
    {
        var fts = new FakeDateTimeService();
        var sut = new DateNotPastValidator(fts);
        var transfer = MakeTransfer.Dummy with
        {
            Date = presentDate.AddDays(-1)
        };
        Assert.That(sut.IsValid(transfer), Is.EqualTo(false));
    }
}
