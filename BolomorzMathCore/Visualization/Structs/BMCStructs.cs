using System.Drawing;

namespace BolomorzMathCore.Visualization.Structs;

public class BMCPoint
{
    public required double X { get; set; }
    public required double Y { get; set; }
}

public class BMCText
{
    public required BMCPoint Position { get; set; }
    public required string Content { get; set; }
    public required double FontSize { get; set; }
}

public class BMCLine
{
    public required BMCPoint StartPosition { get; set; }
    public required BMCPoint EndPosition { get; set; }
    public required double Thickness { get; set; }
}

public class BMCArrow
{
    public required BMCPoint Tip { get; set; }
    public required BMCPoint Left { get; set; }
    public required BMCPoint Right { get; set; }
}

public class BMCCircle
{
    public required BMCPoint Center { get; set; }
    public required double Radius { get; set; }
}

public class BMCRectangle
{
    public required BMCPoint TopLeft { get; set; }
    public required BMCPoint BottomRight { get; set; }
}

public class BMCCanvas
{
    public required double Width { get; set; }
    public required double Height { get; set; }

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

    internal List<BMCText>? Texts { get; set; }
    public BMCText[]? GetTexts()
        => Texts is not null ? [.. Texts] : null;

    internal List<BMCLine>? Lines { get; set; }
    public BMCLine[]? GetLines()
        => Lines is not null ? [.. Lines] : null;

    internal List<BMCArrow>? Arrows { get; set; }
    public BMCArrow[]? GetArrows()
        => Arrows is not null ? [.. Arrows] : null;

    internal List<BMCCircle>? Circles { get; set; }
    public BMCCircle[]? GetCircles()
        => Circles is not null ? [.. Circles] : null;

    internal List<BMCRectangle>? Rectangles { get; set; }
    public BMCRectangle[]? GetRectangles()
        => Rectangles is not null ? [.. Rectangles] : null;
}