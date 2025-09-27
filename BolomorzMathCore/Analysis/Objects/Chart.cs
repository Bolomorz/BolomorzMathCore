namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Chart C
/// 
/// two dimensional chart depicting multiple series (y-axes) in relation to one x-axis
/// 
/// Properties:
/// - Title: String | name/description of C
/// - Series: Series[] | multiple series (y-axes) of C
/// - XAxis: Axis | x-axis of C
/// </code>
/// </summary>
/// <see cref="Analysis.Series"/> 
/// <see cref="Axis"/> 
public class Chart
{
    public required string Title { get; set; }
    public required List<Series> Series { get; set; }
    public required Axis XAxis { get; set; }
}