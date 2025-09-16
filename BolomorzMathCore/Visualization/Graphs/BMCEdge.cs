using BolomorzMathCore.Graphs;
using BolomorzMathCore.Visualization.Structs;

namespace BolomorzMathCore.Visualization.GraphVisualization;

public class BMCEdge : IBMCElement
{
    private Edge Edge;
    private BMCCollection Collection;
    private BMCGraph Graph;

    internal BMCEdge(Edge edge, BMCGraph graph)
    {
        Edge = edge;
        Graph = graph;
        Collection = new();
    }

    #region Interface
    public bool Equals(IBMCElement? element)
    {
        return false;
    }
    public void SetAttributes(Dictionary<string, object> attributes)
    {

    }
    public bool IsPointInGeometry(BMCPoint point)
    {
        return false;
    }
    public bool QueryBy(Dictionary<string, object> query)
    {
        return false;
    }
    public BMCCollection GetBMCCollection()
    {
        return Collection;
    }
    public void Remove()
    {
        
    }
    #endregion
}