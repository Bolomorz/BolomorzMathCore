using BolomorzMathCore.Graphs;
using BolomorzMathCore.Visualization.Base;

namespace BolomorzMathCore.Visualization.GraphVisualization;

public class BMCVertex : BMCElementBase<Vertex>
{
    private BMCGraph Graph;

    internal BMCVertex(Vertex vertex, BMCGraph graph) : base(vertex)
    {
        Graph = graph;
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
}