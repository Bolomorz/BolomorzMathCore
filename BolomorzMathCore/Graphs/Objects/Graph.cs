namespace BolomorzMathCore.Graphs;

public class Graph(GraphType type, GraphWeighting weighting)
{
    private List<Vertex> Vertices { get; set; } = [];
    public List<Vertex> GetVertices() => [.. Vertices];

    private List<Edge> Edges { get; set; } = [];
    public List<Edge> GetEdges() => [.. Edges];

    public GraphType GraphType { get; private set; } = type;
    public GraphWeighting GraphWeighting { get; private set; } = weighting;

    private int NextVID = 0;
    private int NextEID = 0;

    public void CreateVertex(string content)
    {
        Vertices.Add(new(NextVID++, content, this));
    }

    public void CreateEdge(string content, Vertex vertex1, Vertex vertex2, double? weight)
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

    public bool IsInGraph(Vertex vertex)
    {
        for (int i = 0; i < Vertices.Count; i++)
            if (Vertices[i] == vertex)
                return true;
        return false;
    }

    public bool IsInGraph(Edge edge)
    {
        for (int i = 0; i < Edges.Count; i++)
            if (Edges[i] == edge)
                return true;
        return false;
    }

}