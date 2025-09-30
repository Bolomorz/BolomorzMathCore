using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis;

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
    public required Number X { get; set; }
    public required Number Y { get; set; }
}