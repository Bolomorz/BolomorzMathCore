using BolomorzMathCore.Graphs;
using BolomorzMathCore.Visualization.Command;
using BolomorzMathCore.Visualization.Structs;

namespace BolomorzMathCore.Visualization.GraphVisualization;

public class BMCGraph(Graph graph, BMCSettings settings, BMCCanvas canvas)
{
    protected Graph Graph { get; set; } = graph;
    protected BMCCanvas Canvas { get; set; } = canvas;
    protected List<BMCVertex> Vertices { get; set; } = [];
    protected List<BMCEdge> Edges { get; set; } = [];
    protected List<BMCText> Texts { get; set; } = [];
    protected IBMCElement? ActiveElement { get; set; } = null;
    protected BMCSettings Settings { get; set; } = settings;

    public BMCCanvas GetCurrentCanvas()
    {
        return Canvas;
    }
    public IBMCElement? GetActiveElement()
    {
        return ActiveElement;
    }
    public void SetActiveElement(IBMCElement? element)
    {

    }

    public IBMCElement? QueryBy(Dictionary<string, object> query)
    {
        return null;
    }

    public IBMCElement? ElementOfPosition(BMCPoint position)
    {
        return null;
    }

    public void CreateVertex()
    {

    }
    public void CreateEdge()
    {

    }
    public CommandResult Command(Command<GraphCommand> command)
    {
        return new CommandResult();
    }
}