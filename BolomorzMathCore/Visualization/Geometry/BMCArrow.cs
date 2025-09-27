using BolomorzMathCore.Visualization.Base;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Visualization.Geometry;

public class BMCArrow() : BMCGeometryBase(GeometryType.Arrow)
{
    public required BMCPoint Tip { get; set; }
    public required BMCPoint Left { get; set; }
    public required BMCPoint Right { get; set; }

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