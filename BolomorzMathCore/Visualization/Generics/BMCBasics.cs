using System.Drawing;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Visualization.Base;

public class BMCPoint(Number x, Number y)
{
    public Number X { get; private set; } = x;
    public Number Y { get; private set; } = y;

    public void Set(Number? x, Number? y)
    {
        if (x is not null) X = x;
        if (y is not null) Y = y;
        UpdateParent();
    }

    internal void UpdateParent()
    {
        foreach (var parent in Parents)
            parent.Update();
    }

    internal List<IBMCElement> Parents { get; set; } = [];
}

public class BMCCanvas
{
    internal BMCCollection Collection { get; set; } = new();
    public BMCCollection GetCollection() => Collection;
}

public class BMCCollection
{
    internal List<BMCPoint> Points { get; set; } = [];
    public BMCPoint[] GetPoints()
        => [.. Points];

    internal List<BMCGeometryBase> Geometries { get; set; } = [];
    public BMCGeometryBase[] GetGeometries()
        => [.. Geometries];

    internal void Add(BMCCollection other)
    {
        foreach (var point in other.Points)
            Points.Add(point);

        foreach (var geometry in other.Geometries)
            Geometries.Add(geometry);
    }

    internal void Add(BMCPoint point)
    {
        Points.Add(point);
    }

    internal void Add(BMCGeometryBase geometry)
    {
        Geometries.Add(geometry);
    }
}