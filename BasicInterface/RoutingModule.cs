namespace BasicInterface;
public class RoutingModule {
  public ILinkGraph? linkGraph;
  public RoutingModule(ILinkGraph linkGraph) {
    this.linkGraph = linkGraph;
  }
}
