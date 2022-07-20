
namespace CommonTests;
using Common;
public class Tests
{
    [Test]
    public void WnenTransferDateIsFuture_ThenValidationPasses()
    {
        var sut = new DateNotPastValidator();
        var transfer = MakeTransfer.Dummy with
        {
            Date = new DateTime(2025, 3, 12)
        };
        var actual = sut.IsValid(transfer);
        Assert.That(actual, Is.EqualTo(true));
    }
}
