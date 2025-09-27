using BolomorzMathCore.Visualization.Base;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Visualization.Geometry;

public class BMCArc() : BMCGeometryBase(GeometryType.Arc)
{
    public required BMCPoint Center { get; set; }
    public required Number StartAngle { get; set; }
    public required Number SweepAngle { get; set; }
    public required Number Radius { get; set; }

    public override void Reflect(BMCPoint center)
    {
        throw new NotImplementedException();
    }

    public override void Rotate(BMCPoint center, Number angle)
    {
        throw new NotImplementedException();
    }

    public override void Scale(Number factor)
    {
        throw new NotImplementedException();
    }

    public override void Translate(double todo_vector)
    {
        throw new NotImplementedException();
    }
}