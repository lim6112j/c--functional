namespace PlaygroundTests;
using Playground;
public class Tests
{
    static DateTime presentDate = new DateTime(2023, 3, 12);
    /// <summary> pure and fake DateTime service  </summary>
    private class FakeDateTimeService : IDateTimeService
        {
            public DateTime UtcNow => presentDate;
        }
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {

        var svc = new  FakeDateTimeService();
        var sut = new DateNotPastValidator(svc);
        var transfer = MakeTransfer.Dummy with
        {
            Date = presentDate.AddDays(-1)
        };
        var actual = sut.IsValid(transfer);
        Assert.AreEqual(false, actual);
    }
}
