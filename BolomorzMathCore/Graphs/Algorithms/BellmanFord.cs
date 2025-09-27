using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Graphs.Algorithms;

public class BellmanFord : AlgorithmBase<Graph, List<AlgorithmElement<ShortestPath>>>
{
    private Vertex StartVertex;

    public BellmanFord(Graph graph, Vertex startvertex) : base(graph, [])
    {

        if (Input.GraphType != GraphType.Directed || Input.GraphWeighting != GraphWeighting.Weighted)
            throw new Exception("Can only use BellmanFord on Weighted-Directed Graphs.");
        StartVertex = startvertex;

        Init();
        RelaxEdges();
        CheckForNegativeWeightCycles();

    }

    public AlgorithmElement<ShortestPath>? GetResult(Vertex endvertex)
    {

        for (int i = 0; i < Result.Count; i++)
            if (Result[i].Result._Vertex == endvertex)
                return Result[i];

        return null;

    }

    private void Init()
    {

        foreach (var vertex in Input.GetVertices())
        {
            var ae = new AlgorithmElement<ShortestPath>(ShortestPath.BellmanFordElement(vertex));

            if (ae.Result._Vertex == StartVertex)
                ae.Result._Distance = 0;

            Result.Add(ae);

        }
        
    }
    private void RelaxEdges()
    {

        for (int i = 0; i < Result.Count - 1; i++)
        {
            foreach (var edge in Input.GetEdges())
            {
                var aeu = Result.First(e => e.Result._Vertex == edge.Vertex1);
                var aev = Result.First(e => e.Result._Vertex == edge.Vertex2);

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
        foreach(var edge in Input.GetEdges())
        {
            var aeu = Result.First(e => e.Result._Vertex == edge.Vertex1);
            var aev = Result.First(e => e.Result._Vertex == edge.Vertex2);

            if (aeu.Result._Distance + edge.Weight < aev.Result._Distance)
            {

                aev.Result._Predecessor = aeu.Result._Vertex;

                List<bool> visited = new();
                foreach (var ae in Result)
                    visited.Add(false);

                visited[Result.IndexOf(aev)] = true;
                while (!visited[Result.IndexOf(aeu)])
                {
                    visited[Result.IndexOf(aeu)] = true;
                    aeu = Result.First(e => e.Result._Vertex == aeu.Result._Predecessor);
                }

                List<Vertex> ncycle = new() { aeu.Result._Vertex };
                var v = aeu.Result._Predecessor;
                while (v is not null && v != aeu.Result._Vertex)
                {
                    ncycle.Add(v);
                    v = Result.First(e => e.Result._Vertex == v)?.Result._Predecessor;
                }
                throw new Exception("Graph contains a negative-weight cycle");
                
            }
        }
    }
}