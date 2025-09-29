namespace BolomorzMathCore.GraphTheory;

/// <summary>
/// <code>
/// Edge E of Graph G
/// 
/// edge connecting two vertices A, B
/// 
/// Properties:
/// - EID: Number | unique identifier
/// - Content: String | E name or description
/// - Weight: Number | how much it costs to move on E | is null when G.Weighting is NonWeighted
/// - Vertex1: Vertex | Vertex A | StartVertex when G.Type is Directed
/// - Vertex2: Vertex | Vertex B | EndVertex when G.Type is Directed
/// 
/// Methods:
/// - RemoveFromGraph(): remove E from G
/// 
/// Operators Edge A, Edge B:
/// - A is B | A is not B
/// </code>
/// </summary>
/// <see cref="Vertex"/>
/// <see cref="GraphTheory.Graph"/>  
/// <see cref="GraphType"/> 
/// <see cref="GraphWeighting"/> 
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

    /// <summary>
    /// <code>
    /// remove edge from its graph
    /// </code>
    /// </summary>
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
        => !(A == B);

}