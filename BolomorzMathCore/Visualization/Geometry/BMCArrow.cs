using BolomorzMathCore.Visualization.Base;
using BolomorzMathCore.Basics;
using System.Drawing;

namespace BolomorzMathCore.Visualization.Geometry;

public class BMCArrow(IBMCElement owner) : BMCGeometryBase(GeometryType.Arrow, owner)
{
    public required BMCPoint Tip { get; set; }
    public required BMCPoint Left { get; set; }
    public required BMCPoint Right { get; set; }
    public required Color Color { get; set; }

    public override bool IsPointInGeometry(BMCPoint center)
    {
        throw new NotImplementedException();
    }

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