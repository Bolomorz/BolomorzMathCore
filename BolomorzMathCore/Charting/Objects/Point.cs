namespace BolomorzMathCore.Charting;

/// <summary>
/// <code>
/// SeriesPoint SP
/// 
/// point of series S
/// 
/// Properties:
/// - X: Number | x-value
/// - Y: Number | y-value
/// </code>
/// </summary>
/// <see cref="Series"/> 
public class SeriesPoint
{
    public required double X { get; set; }
    public required double Y { get; set; }
}