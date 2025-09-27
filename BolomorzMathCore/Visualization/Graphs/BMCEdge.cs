using BolomorzMathCore.Graphs;
using BolomorzMathCore.Visualization.Base;

namespace BolomorzMathCore.Visualization.GraphVisualization;

public class BMCEdge : BMCElementBase<Edge>
{
    private BMCGraph Graph;

    internal BMCEdge(Edge edge, BMCGraph graph) : base(edge)
    {
        Graph = graph;
    }

    public override bool Equals(BMCElementBase<Edge>? other)
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
}