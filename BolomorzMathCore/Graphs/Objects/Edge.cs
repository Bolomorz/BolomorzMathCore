namespace BolomorzMathCore.Graphs;

public class Edge
{
    public int EID { get; private set; }
    public string Content { get; set; }
    public double? Weight { get; set; }
    public Vertex Vertex1 { get; private set; }
    public Vertex Vertex2 { get; private set; }

    private readonly Graph Graph;

    internal Edge(int eid, string content, Vertex vertex1, Vertex vertex2, Graph graph)
    {
        EID = eid;
        Content = content;
        Vertex1 = vertex1;
        Vertex2 = vertex2;
        Weight = null;
        Graph = graph;
    }

    internal Edge(int eid, string content, Vertex vertex1, Vertex vertex2, double weight, Graph graph)
    {
        EID = eid;
        Content = content;
        Vertex1 = vertex1;
        Vertex2 = vertex2;
        Weight = weight;
        Graph = graph;
    }

    public void RemoveFromGraph()
    {
        Graph.RemoveEdge(this);
    }

    public override bool Equals(object? obj)
        => (obj == null || !(obj is Edge)) ?
            false :
            this == (Edge)obj;

    public override int GetHashCode()
        => EID * Content.GetHashCode();

    public override string ToString()
        => $"Edge {EID}: {Content}";

    public static bool operator ==(Edge A, Edge B)
        => A.EID == B.EID;

    public static bool operator !=(Edge A, Edge B)
        => A.EID != B.EID;

}