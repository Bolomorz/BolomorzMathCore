namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Axis A
/// 
/// axis of chart C in either y or x direction
/// in y direction belongs to series S
/// 
/// Properties:
/// - Name: String | name/description of A
/// - Unit: String | unit of measurement
/// - LastVal: Number | value of last number added to S
/// - Min: Number | current minimum value of selected area in C
/// - Max: Number | current maximum value of selected area in C
/// - DefaultMin: Number | default min value 
/// - DefaultMax: Number | default max value
/// 
/// Methods:
/// - SetMin: set Min value | current minimum value of selected area in C
/// - SetMax: set Max value | current maximum value of selected area in C
/// - ResetMin: set Min value to DefaultMin
/// - ResetMax: set Max value to DefaultMax
/// </code>
/// </summary>
/// <see cref="Series"/> 
/// <see cref="Chart"/> 
public class Axis(string name, string unit, double min, double max)
{
    public string Name { get; private set; } = name;
    public string Unit { get; private set; } = unit;
    public double LastVal { get; private set; } = double.MinValue;
    public double Min { get; private set; } = min;
    public double Max { get; private set; } = max;
    public double DefaultMin { get; private set; } = min;
    public double DefaultMax { get; private set; } = max;

    /// <summary>
    /// <code>
    /// SetMin: set Min value | current minimum value of selected area in C
    /// </code>
    /// </summary>
    public void SetMin(double min)
    {
        if (min > Max) return;
        Min = min;
    }
    /// <summary>
    /// <code>
    /// SetMax: set Max value | current maximum value of selected area in C
    /// </code>
    /// </summary>
    public void SetMax(double max)
    {
        if (max < Min) return;
        Max = max;
    }
    /// <summary>
    /// <code>
    /// ResetMin: set Min value to DefaultMin | current minimum value of selected area in C
    /// </code>
    /// </summary>
    public void ResetMin()
    {
        Min = DefaultMin;
    }
    /// <summary>
    /// <code>
    /// ResetMax: set Max value to DefaultMax | current maximum value of selected area in C
    /// </code>
    /// </summary>
    public void ResetMax()
    {
        Max = DefaultMax;
    }

    internal void CompareMax(double value)
    {
        if (value > Max)
        {
            double val = 1;

            while (value > val)
                val *= 10;
            val = val == 1 ? 1 : val / 10;

            Max = 0;
            while (value > Max)
                Max += val;

            DefaultMax = Max;
        }
    }
    internal void CompareMin(double value)
    {
        if (value < Min)
        {
            double val;
            if (value < 0)
            {
                val = -1;

                while (value < val)
                    val *= 10;

                val = val == -1 ? -1 : val / 10;

                Min = 0;
                while (value < Min)
                    Min += val;
            }
            else
            {
                val = 1;

                while (value > val)
                    val += 10;

                val = val == 1 ? 1 : val / 10;

                Min = 0;
                while (value > Min + val)
                    Min += val;
            }

            if (Min < 0)
            {
                if ((-1) * Min < Max / 10)
                    Min = (-1) * (Max / 10);
            }
            else
            {
                if (Min < Max / 10)
                    Min = 0;
            }

            DefaultMin = Min;
        }
    }
}