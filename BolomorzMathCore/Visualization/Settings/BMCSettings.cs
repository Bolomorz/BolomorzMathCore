using System.Drawing;

namespace BolomorzMathCore.Visualization;

public class BMCSettings
{
    #region Canvas
    public double PaddingLeft { get; set; } = 0.01;
    public double PaddingRight { get; set; } = 0.01;
    public double PaddingTop { get; set; } = 0.01;
    public double PaddingBot { get; set; } = 0.01;
    #endregion

    #region Charting
    public double GridIntervall { get; set; } = 0.05;
    public double YAxisWidth { get; set; } = 0.05;
    #endregion

    #region Graph
    public double VertexRadius { get; set; } = 0.10;
    public double FontSize { get; set; } = 0.03;
    public Color ActiveColor { get; set; } = Color.LightBlue;
    public Color InactiveColor { get; set; } = Color.Black;
    #endregion

    #region Linear Algebra
    public int Precision { get; set; } = 2;
    #endregion
}