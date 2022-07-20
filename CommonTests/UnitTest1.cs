
namespace CommonTests;
using Common;
public class Tests
{
    readonly DateTime today = new(2022, 7, 20);
    [TestCase(+1, ExpectedResult = true)]
    [TestCase(0, ExpectedResult = true)]
    [TestCase(-1, ExpectedResult = false)]
    public bool TestWithModified(int offset)
    {
        var sut = new DateNotPastValidator(() => today);
        var transfer = MakeTransfer.Dummy with
        {
            Date = today.AddDays(offset)
        };
        return sut.IsValid(transfer);
    }
}
