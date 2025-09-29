using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Visualization.Base;

public enum GeometryType { Line, Arc, Arrow, Polygon, Text }

public abstract class BMCGeometryBase(GeometryType type, IBMCElement owner)
{
    protected IBMCElement Owner = owner;
    protected GeometryType Type = type;
    public new GeometryType GetType() => Type;
    public abstract void Rotate(BMCPoint center, Number angle);
    public abstract void Translate(double todo_vector);
    public abstract void Scale(Number factor);
    public abstract void Reflect(BMCPoint center);
    public abstract bool IsPointInGeometry(BMCPoint center);
}