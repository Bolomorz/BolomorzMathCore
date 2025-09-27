using BolomorzMathCore.Basics;

namespace BolomorzMathCore.GraphTheory.Algorithms;

public class Dijkstra : AlgorithmBase<Graph, List<AlgorithmElement<ShortestPath>>>
{
    private List<Vertex> Q;
    private Vertex StartVertex;

    public Dijkstra(Graph graph, Vertex startvertex) : base(graph, [])
    {

        if (Input.GraphType != GraphType.Directed || Input.GraphWeighting != GraphWeighting.Weighted)
            throw new Exception("Can only use Dijkstra on Weighted Directed Graph");

        Q = [];
        StartVertex = startvertex;

        Init();
        DijkstraCalculation();

        foreach (var ae in Result)
            ae.Result._Path = ShortestPath(ae.Result._Vertex);
        
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

            var ae = new AlgorithmElement<ShortestPath>(Algorithms.ShortestPath.DijkstraElement(vertex));

            if (vertex == StartVertex)
                ae.Result._Distance = 0;
            Result.Add(ae);
            Q.Add(vertex);

        }
        
    }
    private void DijkstraCalculation()
    {

        while (Q.Count > 0)
        {

            var index = IndexOfVertexWithSmallestDistanceInQ();
            if (index == -1) break;

            var u = Q[index];
            Q.Remove(u);
            foreach (var v in u.Adjacents)
                if (IsInQ(v))
                    DistanceUpdate(u, v);

        }
        
    }
    private int IndexOfVertexWithSmallestDistanceInQ()
    {

        var dist = double.PositiveInfinity;
        var index = -1;
        
        for (int i = 0; i < Q.Count; i++)
        {
            var u = Q[i];
            var ae = Result.First(e => e.Result._Vertex == u);
            if (ae is not null && ae.Result._Distance < dist)
            {
                index = i;
                dist = ae.Result._Distance;
            }
        }
        return index;
    }
    private bool IsInQ(Vertex v)
    {

        foreach (var q in Q)
            if (q == v)
                return true;

        return false;
        
    }
    private void DistanceUpdate(Vertex u, Vertex v)
    {

        var aeu = Result.First(e => e.Result._Vertex == u);
        var aev = Result.First(e => e.Result._Vertex == v);

        if (aeu is not null && aev is not null)
        {
            var alt = aeu.Result._Distance + WeightingOfEdge(u, v);
            if (alt is not null && alt < aev.Result._Distance)
            {
                aev.Result._Distance = (double)alt;
                aev.Result._Predecessor = u;
            }
        }
        
    }
    private double? WeightingOfEdge(Vertex u, Vertex v)
    {

        foreach (var edge in Input.GetEdges())
            if (edge.Vertex1 == u && edge.Vertex2 == v)
                return edge.Weight;

        return null;
        
    }
    private List<Vertex> ShortestPath(Vertex v)
    {

        List<Vertex> path = [v];

        var u = v;
        var ae = Result.First(e => e.Result._Vertex == v);

        while (ae is not null)
        {
            if (ae.Result._Predecessor is null) break;
            else
            {
                u = ae.Result._Predecessor;
                path.Insert(0, u);
                ae = Result.First(e => e.Result._Vertex == u);
            }
        }

        return path;
        
    }
}