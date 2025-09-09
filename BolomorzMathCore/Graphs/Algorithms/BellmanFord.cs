namespace BolomorzMathCore.Graphs.Algorithms;

public class BellmanFord : IGraphAlgorithm<ShortestPath>
{
    private List<AlgorithmElement<ShortestPath>> Elements;
    private Graph Graph;
    private Vertex StartVertex;

    public BellmanFord(Graph graph, Vertex startvertex)
    {

        if (graph.GraphType != GraphType.Directed || graph.GraphWeighting != GraphWeighting.Weighted)
            throw new Exception("Can only use BellmanFord on Weighted-Directed Graphs.");

        Elements = new();
        Graph = graph;
        StartVertex = startvertex;

        Init();
        RelaxEdges();
        CheckForNegativeWeightCycles();

    }

    public List<AlgorithmElement<ShortestPath>> GetResult()
        => Elements;

    public AlgorithmElement<ShortestPath>? GetResult(Vertex endvertex)
    {

        for (int i = 0; i < Elements.Count; i++)
            if (Elements[i].Result._Vertex == endvertex)
                return Elements[i];

        return null;

    }

    private void Init()
    {

        foreach (var vertex in Graph.GetVertices())
        {
            var ae = new AlgorithmElement<ShortestPath>(ShortestPath.BellmanFordElement(vertex));

            if (ae.Result._Vertex == StartVertex)
                ae.Result._Distance = 0;

            Elements.Add(ae);

        }
        
    }
    private void RelaxEdges()
    {

        for (int i = 0; i < Elements.Count - 1; i++)
        {
            foreach (var edge in Graph.GetEdges())
            {
                var aeu = Elements.First(e => e.Result._Vertex == edge.Vertex1);
                var aev = Elements.First(e => e.Result._Vertex == edge.Vertex2);

                if (aeu is not null && aev is not null)
                {
                    if (edge.Weight is not null && aeu.Result._Distance + edge.Weight < aev.Result._Distance)
                    {
                        aev.Result._Distance = aeu.Result._Distance + (double)edge.Weight;
                        aev.Result._Predecessor = aeu.Result._Vertex;
                    }
                }
            }
        }
        
    }
    private void CheckForNegativeWeightCycles()
    {
        foreach(var edge in Graph.GetEdges())
        {
            var aeu = Elements.First(e => e.Result._Vertex == edge.Vertex1);
            var aev = Elements.First(e => e.Result._Vertex == edge.Vertex2);

            if (aeu.Result._Distance + edge.Weight < aev.Result._Distance)
            {

                aev.Result._Predecessor = aeu.Result._Vertex;

                List<bool> visited = new();
                foreach (var ae in Elements)
                    visited.Add(false);

                visited[Elements.IndexOf(aev)] = true;
                while (!visited[Elements.IndexOf(aeu)])
                {
                    visited[Elements.IndexOf(aeu)] = true;
                    aeu = Elements.First(e => e.Result._Vertex == aeu.Result._Predecessor);
                }

                List<Vertex> ncycle = new() { aeu.Result._Vertex };
                var v = aeu.Result._Predecessor;
                while (v is not null && v != aeu.Result._Vertex)
                {
                    ncycle.Add(v);
                    v = Elements.First(e => e.Result._Vertex == v)?.Result._Predecessor;
                }
                throw new Exception("Graph contains a negative-weight cycle");
                
            }
        }
    }
}