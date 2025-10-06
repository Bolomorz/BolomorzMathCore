namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Chart C
/// 
/// two dimensional chart depicting multiple relations (y-axes) in relation to one x-axis
/// 
/// Properties:
/// - Title: String | name/description of C
/// - Relations: Relation[] | multiple relations (y-axes) of C
/// - XAxis: Axis | x-axis of C
/// </code>
/// </summary>
/// <see cref="NRelation"/> 
/// <see cref="Axis"/> 
public class Chart
{
    public required string Title { get; set; }
    public required List<NRelation> Relations { get; set; }
    public required NAxis XAxis { get; set; }
}