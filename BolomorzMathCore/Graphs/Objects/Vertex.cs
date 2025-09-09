namespace BolomorzMathCore.Graphs;

public class Vertex
{
    public int VID { get; private set; }
    public string Content { get; set; }

    internal List<Vertex> Adjacents { get; private set; }

    private readonly Graph Graph;

    internal Vertex(int vid, string content, Graph graph)
    {
        VID = vid;
        Content = content;
        Adjacents = [];
        Graph = graph;
    }

    public void RemoveFromGraph()
    {
        Graph.RemoveVertex(this);
    }

    internal int? GetAdjacentIndex(Vertex adjacent)
    {
        for (int i = 0; i < Adjacents.Count; i++)
            if (Adjacents[i].VID == adjacent.VID)
                return i;
        return null;
    }

    internal void AddAdjacent(Vertex adjacent)
    {
        if (GetAdjacentIndex(adjacent) is null)
            Adjacents.Add(adjacent);
    }

    internal void RemoveAdjacent(Vertex adjacent)
    {
        Adjacents.Remove(adjacent);
    }

    public override bool Equals(object? obj)
        => (obj == null || !(obj is Vertex)) ?
            false :
            this == (Vertex)obj;

    public override int GetHashCode()
        => VID * Content.GetHashCode();

    public override string ToString()
        => $"Vertex {VID}: {Content}";

    public static bool operator ==(Vertex? A, Vertex? B)
        => A?.VID == B?.VID;

    public static bool operator !=(Vertex? A, Vertex? B)
        => A?.VID != B?.VID;
}