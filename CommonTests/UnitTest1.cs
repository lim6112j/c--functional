
namespace CommonTests;
using Common;
public class Tests
{
    readonly DateTime today = new(2025, 3, 12);
    [Test]
    public void TestWithModified()
    {
        var sut = new DateNotPastValidator(() => today);
        var transfer = MakeTransfer.Dummy with
        {
            Date = today
        };
        Assert.That(sut.IsValid(transfer), Is.EqualTo(true));
    }
}
