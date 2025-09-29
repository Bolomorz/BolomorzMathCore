using System.Drawing;
using BolomorzMathCore.GraphTheory;
using BolomorzMathCore.Visualization.Base;
using BolomorzMathCore.Visualization.Geometry;

namespace BolomorzMathCore.Visualization.GraphVis;

public class BMCVertex : BMCElementBase<Vertex>
{
    private BMCGraph Graph;
    private BMCPoint Center;

    internal BMCArc Circle;
    internal BMCText Description;

    internal BMCVertex(Vertex vertex, BMCGraph graph, BMCPoint center) : base(vertex)
    {
        Graph = graph;
        Center = center;
        Center.Parents.Add(this);

        Circle = new BMCArc(this)
        {
            Center = new BMCPoint(Center.X, Center.Y),
            StartAngle = new(0),
            SweepAngle = new(2 * Math.PI),
            Radius = Graph.Settings.VertexRadius,
            Color = Graph.Settings.InactiveColor
        };

        Description = new BMCText(this)
        {
            Position = new BMCPoint(Center.X, Center.Y),
            Content = vertex.Content,
            FontSize = Graph.Settings.FontSize,
            Rotation = new(0),
            Color = Graph.Settings.InactiveColor
        };
    }

    public override bool Equals(IBMCElement? other)
    {
        throw new NotImplementedException();
    }

    public override BMCCollection GetBMCCollection()
    {
        throw new NotImplementedException();
    }

    public override bool IsPointInGeometry(BMCPoint point)
    {
        throw new NotImplementedException();
    }

    public override bool QueryBy(Dictionary<string, object> query)
    {
        throw new NotImplementedException();
    }

    public override void Remove()
    {
        throw new NotImplementedException();
    }

    public override void SetAttributes(Dictionary<string, object> attributes)
    {
        throw new NotImplementedException();
    }

    public override void Update()
    {
        Circle.Center.Set(Center.X, Center.Y);
        Description.Position.Set(Center.X, Center.Y);
    }

    public override void SetPosition(BMCPoint center)
    {
        Center.Set(center.X, center.Y);
    }
}