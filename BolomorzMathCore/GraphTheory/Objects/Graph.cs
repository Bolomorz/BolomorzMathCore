namespace BolomorzMathCore.GraphTheory;

/// <summary>
/// <code>
/// Graph G
/// 
/// graph consisting of vertices and edges
/// 
/// Properties:
/// - GraphType: GraphType | Directed or NonDirected
/// - GraphWeighting: GraphWeighting | Weighted or NonWeighted
/// 
/// Getters:
/// - GetVertices: Vertex[]
/// - GetEdges: Edge[]
/// 
/// Methods:
/// - CreateVertex(content): 
///     create vertex with name/description
/// - CreateEdge(content, vertex1, vertex2, weighting): 
///     create edge between vertex1 and vertex2 with name/description and weighting
/// - IsInGraph(A): 
///     Graph G contains vertex/edge A ?
/// </code>
/// </summary>
/// <see cref="Vertex"/> 
/// <see cref="Edge"/>
/// <see cref="GraphTheory.GraphType"/> 
/// <see cref="GraphTheory.GraphWeighting"/> 
public class Graph(GraphType type, GraphWeighting weighting)
{
    public List<Vertex> Vertices { get; set; } = [];
    /// <summary>
    /// <code>
    /// GetVertices: Vertex[] | vertices of graph
    /// </code>
    /// </summary>
    public List<Vertex> GetVertices() => [.. Vertices];

    public List<Edge> Edges { get; set; } = [];
    /// <summary>
    /// <code>
    /// GetEdges: Edge[] | edges of graph
    /// </code>
    /// </summary>
    public List<Edge> GetEdges() => [.. Edges];

    public GraphType GraphType { get; private set; } = type;
    public GraphWeighting GraphWeighting { get; private set; } = weighting;

    private int NextVID = 0;
    private int NextEID = 0;

    /// <summary>
    /// <code>
    /// CreateVertex(content): 
    ///     create vertex with name/description
    /// </code>
    /// </summary>
    public Vertex CreateVertex(string content)
    {
        var vertex = new Vertex(NextVID++, content, this);
        Vertices.Add(vertex);
        return vertex;
    }

    /// <summary>
    /// <code>
    /// CreateEdge(content, vertex1, vertex2, weighting): 
    ///     create edge between vertex1 and vertex2 with name/description and weighting
    /// </code>
    /// </summary>
    public Edge CreateEdge(string content, Vertex vertex1, Vertex vertex2, double? weight)
    {

        if (!IsInGraph(vertex1) || !IsInGraph(vertex2))
            throw new Exception("can only create edges between vertices belonging to this graph");

        var edge = GraphWeighting == GraphWeighting.Weighted && weight is not null ?
            new Edge(NextEID++, content, vertex1, vertex2, (double)weight, this) :
            new Edge(NextEID++, content, vertex1, vertex2, this);

        switch (GraphType)
        {
            case GraphType.Directed:
                edge.Vertex1.AddAdjacent(edge.Vertex2);
                Edges.Add(edge);
                break;
            case GraphType.Undirected:
                edge.Vertex1.AddAdjacent(edge.Vertex2);
                edge.Vertex2.AddAdjacent(edge.Vertex1);
                Edges.Add(edge);
                break;
        }

        return edge;

    }

    internal void RemoveEdge(Edge edge)
    {

        var oldedge = Edges.FirstOrDefault(e => e == edge);

        if (oldedge is not null)
        {

            switch (GraphType)
            {
                case GraphType.Directed:
                    oldedge.Vertex1.RemoveAdjacent(oldedge.Vertex2);
                    break;
                case GraphType.Undirected:
                    oldedge.Vertex1.RemoveAdjacent(oldedge.Vertex2);
                    oldedge.Vertex2.RemoveAdjacent(oldedge.Vertex1);
                    break;
            }

            Edges.Remove(oldedge);

        }

    }

    internal void RemoveVertex(Vertex vertex)
    {

        var oldvertex = Vertices.FirstOrDefault(v => v == vertex);

        if (oldvertex is not null)
        {

            var edges = Edges.Where(e => e.Vertex1 == vertex || e.Vertex2 == vertex).ToList();
            foreach (var edge in edges)
                RemoveEdge(edge);

            Vertices.Remove(oldvertex);

        }

    }

    /// <summary>
    /// <code>
    /// IsInGraph(A): 
    ///     graph contains vertex A ?
    /// </code>
    /// </summary>
    public bool IsInGraph(Vertex vertex)
    {
        for (int i = 0; i < Vertices.Count; i++)
            if (Vertices[i] == vertex)
                return true;
        return false;
    }

    /// <summary>
    /// <code>
    /// IsInGraph(A): 
    ///     graph contains edge A ?
    /// </code>
    /// </summary>
    public bool IsInGraph(Edge edge)
    {
        for (int i = 0; i < Edges.Count; i++)
            if (Edges[i] == edge)
                return true;
        return false;
    }

}