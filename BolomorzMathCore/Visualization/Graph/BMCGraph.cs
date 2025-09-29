using BolomorzMathCore.GraphTheory;
using BolomorzMathCore.Visualization.Command;
using BolomorzMathCore.Visualization.Base;

namespace BolomorzMathCore.Visualization.GraphVis;

public class BMCGraph : BMCGroupBase<Graph, GraphCommand>
{
    protected List<BMCVertex> Vertices { get; set; } = [];
    protected List<BMCEdge> Edges { get; set; } = [];
    protected List<Geometry.BMCText> Texts { get; set; } = [];

    public BMCGraph(Graph graph, BMCSettings settings, BMCGraphPositions positions) :
    base(graph, settings)
    {
        foreach (var vertex in Value.GetVertices())
        {
            var pos = positions.VertexPositions.GetValueOrDefault(vertex);
            Vertices.Add
            (
                pos is not null ? new BMCVertex(vertex, this, pos) : new BMCVertex(vertex, this, new(Settings.DefaultPosition, Settings.DefaultPosition))
            );
        }

        foreach (var edge in Value.GetEdges())
        {
            var pos = positions.EdgePositions.GetValueOrDefault(edge);
            Edges.Add
            (
                pos is not null ? new BMCEdge(edge, this, pos, Vertices) : new BMCEdge(edge, this, new(Settings.DefaultPosition, Settings.DefaultPosition), Vertices)
            );
        }

        foreach (var text in positions.Texts)
            Texts.Add(text);

    }

    public override BMCCanvas GetCanvas()
    {
        BMCCollection collection = new();

        foreach (var vertex in Vertices)
            collection.Add(vertex.GetBMCCollection());

        foreach (var edge in Edges)
            collection.Add(edge.GetBMCCollection());

        foreach (var text in Texts)
            collection.Add(text);

        return new BMCCanvas() { Collection = collection };
    }

    public override IBMCElement? GetActiveElement()
    {
        return ActiveElement;
    }

    public override void SetActiveElement(IBMCElement? element)
    {
        if (element is not null)
            ActiveElement = element;
    }

    public override IBMCElement? ElementOfPosition(BMCPoint position)
    {
        foreach (var vertex in Vertices)
            if (vertex.IsPointInGeometry(position))
                return vertex;

        foreach (var edge in Edges)
            if (edge.IsPointInGeometry(position))
                return edge;

        return null;
    }

    public override IBMCElement? QueryBy(Dictionary<string, object> query)
    {
        foreach (var vertex in Vertices)
            if (vertex.QueryBy(query))
                return vertex;

        foreach (var edge in Edges)
            if (edge.QueryBy(query))
                return edge;

        return null;
    }

    public override CommandResult Command(Command<GraphCommand> command)
    {
        switch (command.DoCommand)
        {
            case GraphCommand.CreateVertex:

                var valueCV = command.Values?.GetValueOrDefault("Content");
                var contentV = valueCV is not null && valueCV.GetType() == typeof(string) ? (string)valueCV : null;
                if (contentV is null) throw new Exception("did not find suitable value for 'Content'");

                var valuePV = command.Values?.GetValueOrDefault("Position");
                var posV = valuePV is not null && valuePV.GetType() == typeof(BMCPoint) ? (BMCPoint)valuePV : null;
                if (posV is null) throw new Exception("did not find suitable value for 'Position'");

                var vertex = Value.CreateVertex(contentV);
                Vertices.Add(new(vertex, this, posV));
                return new() { Lines = ["vertex created."] };

            case GraphCommand.CreateEdge:
                var valueCE = command.Values?.GetValueOrDefault("Content");
                var contentE = valueCE is not null && valueCE.GetType() == typeof(string) ? (string)valueCE : null;

                var edge = 0;
                return new() { Lines = ["edge created."] };

            default:
                return new() { Lines = ["unknown command."] };
        }
    }
}