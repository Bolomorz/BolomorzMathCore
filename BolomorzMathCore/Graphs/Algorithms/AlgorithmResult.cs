namespace BolomorzMathCore.Graphs.Algorithms;

public class AlgorithmElement<T>(T result)
{
    public T Result { get; private set; } = result;
}

public class ShortestPath
{
    internal Vertex _Vertex { get; set; }
    public Vertex Vertex => _Vertex;

    internal double _Distance { get; set; }
    public double Distance => _Distance;

    internal Vertex? _Predecessor { get; set; }
    public Vertex? Predecessor => _Predecessor;

    internal List<Vertex>? _Path { get; set; }
    public List<Vertex>? Path => _Path;

    internal ShortestPath(Vertex vertex, double distance, List<Vertex>? shortestpath)
    {
        _Vertex = vertex;
        _Distance = distance;
        _Predecessor = null;
        _Path = shortestpath;
    }

    internal static ShortestPath DijkstraElement(Vertex vertex)
        => new(vertex, double.PositiveInfinity, []);

    internal static ShortestPath BellmanFordElement(Vertex vertex)
        => new(vertex, double.PositiveInfinity, null);

}