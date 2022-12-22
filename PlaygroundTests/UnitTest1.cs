namespace PlaygroundTests;
using Playground;
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        var sut = new DateNotPastValidator();
        var transfer = MakeTransfer.Dummy with
        {
            Date = new DateTime(2023, 3, 12)
        };
        var actual = sut.IsValid(transfer);
        Assert.AreEqual(true, actual);
    }
}
