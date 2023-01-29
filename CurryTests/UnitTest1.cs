using Curry;

namespace CurryTests;
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    [Test]
    public void CurryTests() {
      var curriedFunc = Extension.greet.CurryFunc();
      Assert.AreEqual(curriedFunc("hello")("ciel"), "hello, ciel");
    }
}
