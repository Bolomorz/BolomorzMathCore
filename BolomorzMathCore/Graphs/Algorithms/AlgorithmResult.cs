namespace BolomorzMathCore.Graphs.Algorithms;

public interface IGraphAlgorithm<T>
{
    List<AlgorithmElement<T>> GetResult();
    AlgorithmElement<T>? GetResult(Vertex endvertex);
}

public class AlgorithmElement<T>
{
    public T Result { get; private set; }

    public AlgorithmElement(T result)
    {
        Result = result;
    }
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