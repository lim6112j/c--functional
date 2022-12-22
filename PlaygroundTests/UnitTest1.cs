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

        var svc = new FakeDateTimeService();
        var sut = new DateNotPastValidator(svc);
        var transfer = MakeTransfer.Dummy with
        {
            Date = presentDate.AddDays(-1)
        };
        var actual = sut.IsValid(transfer);
        Assert.AreEqual(false, actual);
    }
    readonly DateTime today = new(2023, 3, 12);
    [Test]
    public void Test2()
    {
        var sut = new DateNotPastValidatorFunc(() => today);
        var transfer = MakeTransfer.Dummy with { Date = today };
        Assert.AreEqual(true, sut.IsValid(transfer));
    }
    [Test]
    public void Test3()
    {
        var sut = new DateNotPastValidatorDelegate(() => today);
        var transfer = MakeTransfer.Dummy with { Date = today };
        Assert.AreEqual(true, sut.IsValid(transfer));
    }
    [TestCase(+1, ExpectedResult = true)]
    [TestCase(+0, ExpectedResult = true)]
    [TestCase(-1, ExpectedResult = false)]
    public bool WhenTransferDateIsPast_ThenValidatorFails(int offset)
        {
            var sut = new DateNotPastValidatorDelegate(()  => presentDate);
            var transfer = MakeTransfer.Dummy with {
                Date = presentDate.AddDays(offset)
            };
            return sut.IsValid(transfer);
        }
}
