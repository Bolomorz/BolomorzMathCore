namespace BolomorzMathCore.Charting;

public class Chart
{
    public required string Title { get; set; }
    public required List<Series> Series { get; set; }
    public required Axis XAxis { get; set; }
    public required ChartingSettings Settings { get; set; }
}