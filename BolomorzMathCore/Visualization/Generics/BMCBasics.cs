using System.Drawing;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Visualization.Base;

public class BMCPoint
{
    public required Number X { get; set; }
    public required Number Y { get; set; }
}

public class BMCCanvas
{
    public required Number Width { get; set; }
    public required Number Height { get; set; }

    internal List<BMCCollection> Collections { get; set; } = [];
    public BMCCollection[] GetCollections() => [.. Collections];

    internal void Clear()
    {
        Collections.Clear();
    }
}

public class BMCCollection
{
    internal Color Color { get; set; }
    public Color GetColor()
        => Color;

    internal List<BMCPoint>? Points { get; set; }
    public BMCPoint[]? GetPoints()
        => Points is not null ? [.. Points] : null;

    internal List<BMCGeometryBase>? Geometries { get; set; }
    public BMCGeometryBase[]? GetGeometries()
        => Geometries is not null ? [.. Geometries] : null;
}