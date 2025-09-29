using BolomorzMathCore.GraphTheory;
using BolomorzMathCore.Visualization.Base;

namespace BolomorzMathCore.Visualization.GraphVis;

public class BMCEdge : BMCElementBase<Edge>
{
    private BMCGraph Graph;
    private BMCPoint Center;

    internal BMCEdge(Edge edge, BMCGraph graph, BMCPoint center, List<BMCVertex> vertices) : base(edge)
    {
        Graph = graph;
        Center = center;
    }

    public override bool Equals(IBMCElement? other)
    {
        throw new NotImplementedException();
    }

    public override BMCCollection GetBMCCollection()
    {
        throw new NotImplementedException();
    }

    public override bool IsPointInGeometry(BMCPoint point)
    {
        throw new NotImplementedException();
    }

    public override bool QueryBy(Dictionary<string, object> query)
    {
        throw new NotImplementedException();
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }

    public override void SetAttributes(Dictionary<string, object> attributes)
    {
        throw new NotImplementedException();
    }

    public override void SetPosition(BMCPoint center)
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        throw new NotImplementedException();
    }
}