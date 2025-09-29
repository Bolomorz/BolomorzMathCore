using BolomorzMathCore.GraphTheory;
using BolomorzMathCore.Visualization.Base;
using BolomorzMathCore.Visualization.Geometry;

namespace BolomorzMathCore.Visualization.GraphVis;

public class BMCGraphPositions
{
    public Dictionary<Vertex, BMCPoint> VertexPositions { get; set; } = [];
    public Dictionary<Edge, BMCPoint> EdgePositions { get; set; } = [];
    public List<BMCText> Texts { get; set; } = [];
}