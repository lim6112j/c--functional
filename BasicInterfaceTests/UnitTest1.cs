namespace BasicInterfaceTests;
using BasicInterface;
public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
    [Test]
    public void InterfaceTest1()
    {
      ILinkGraph lg = new LinkGraph();
      RoutingModule rm = new RoutingModule(lg);
      Assert.NotNull(rm.linkGraph , "pass");
    }
}
