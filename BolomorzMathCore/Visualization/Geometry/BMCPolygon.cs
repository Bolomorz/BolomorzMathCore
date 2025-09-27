using BolomorzMathCore.Visualization.Base;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Visualization.Geometry;

public class BMCPolygon : BMCGeometryBase
{
    public required BMCPoint[] Points { get; set; }

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