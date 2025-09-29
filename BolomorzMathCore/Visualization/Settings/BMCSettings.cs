using System.Drawing;
using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Visualization;

public class BMCSettings
{
    #region Canvas
    public required Number Width { get; set; }
    public required Number Height { get; set; }
    public Number PaddingLeft { get; set; } = new(0.01);
    public Number PaddingRight { get; set; } = new(0.01);
    public Number PaddingTop { get; set; } = new(0.01);
    public Number PaddingBot { get; set; } = new(0.01);
    public Number DefaultPosition { get; set; } = new(-1);
    #endregion

    #region Charting
    public Number GridIntervall { get; set; } = new(0.05);
    public Number YAxisWidth { get; set; } = new(0.05);
    #endregion

    #region Graph
    public Number VertexRadius { get; set; } = new(0.10);
    public Number FontSize { get; set; } = new(0.03);
    public Color ActiveColor { get; set; } = Color.LightBlue;
    public Color InactiveColor { get; set; } = Color.Black;
    #endregion

    #region Linear Algebra
    public Number Precision { get; set; } = new(2);
    #endregion
}