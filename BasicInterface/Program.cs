namespace BasicInterface;
public class Program {
  public static void Main() {

    ILinkGraph linkGraph = new LinkGraph();
    RoutingModule rm = new RoutingModule(linkGraph);
    rm.linkGraph?.getInfo();

    ILinkGraph linkGraph2 = new LinkGraph2();
    rm = new RoutingModule(linkGraph2);
    rm.linkGraph?.getInfo();
  }
}
