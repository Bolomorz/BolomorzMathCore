using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis;

/// <summary>
/// <code>
/// Axis A{T} where T is Number/Complex
/// 
/// axis on chart C in x or y direction.
/// 
/// Properties:
/// - Name: String | name/description of axis
/// - Unit: String | unit represented by values of axis
/// - Min: T | currently set min value of displayed area
/// - Max: T | currently set max value of displayed area
/// 
/// Methods:
/// - SetMin: set minimum value of displayed area | cannot be more than Max
/// - SetMax: set maximum value of displayed area | cannot be less than Min
/// - ResetMin: reset minimum value of displayed area to default value
/// - ResetMax: reset maximum value of displayed area to default value
/// 
/// when a new-value is added to the relation this axis belongs to, 
/// the default min/max values will be recalculated only if new-value is less than Min / more than Max
/// - Max sets itself to next digit greater than new-value, if new-value > Max
///     f.e.    new-value = 15 => Max = DefaultMax = 20
/// - Min sets itself to next digit less than new-value, but only if its at least 1/10 of Max, else its 0
///     f.e.    new-value = 15 AND Max = 20 => Min = DefaultMin = 10
///             new-value = 15 AND Max = 200 => Min = DefaultMin = 0 (not 10)
/// </code>
/// </summary>
public abstract class AxisBase<T>(string name, string unit, T min, T max) where T : class
{
    /// <summary>
    /// <code>
    /// Name: String | name/description of axis
    /// </code>
    /// </summary>
    public string Name { get; set; } = name;
    /// <summary>
    /// <code>
    /// Unit: String | unit represented by values of axis
    /// </code>
    /// </summary>
    public string Unit { get; set; } = unit;
    /// <summary>
    /// <code>
    /// Min: T | currently set min value of displayed area
    /// </code>
    /// </summary>
    public T Min { get; protected set; } = min;
    /// <summary>
    /// <code>
    /// Max: T | currently set max value of displayed area
    /// </code>
    /// </summary>
    public T Max { get; protected set; } = max;
    protected T DefaultMin = min;
    protected T DefaultMax = max;

    /// <summary>
    /// <code>
    /// SetMin: set minimum value of displayed area | cannot be more than Max
    /// </code>
    /// </summary>
    public abstract void SetMin(T min);
    /// <summary>
    /// <code>
    /// SetMax: set maximum value of displayed area | cannot be less than Min
    /// </code>
    /// </summary>
    public abstract void SetMax(T max);
    /// <summary>
    /// <code>
    /// ResetMin: reset minimum value of displayed area to default value
    /// </code>
    /// </summary>
    public void ResetMin()
    {
        Min = DefaultMin;
    }
    /// <summary>
    /// <code>
    /// ResetMax: reset maximum value of displayed area to default value
    /// </code>
    /// </summary>
    public void ResetMax()
    {
        Max = DefaultMax;
    }

    /// <summary>
    /// <code>
    /// when a new-value is added to the relation this axis belongs to, 
    /// the default min/max values will be recalculated only if new-value is less than Min / more than Max
    /// - Max sets itself to next digit greater than new-value, if new-value > Max
    ///     f.e.    new-value = 15 => Max = DefaultMax = 20
    /// </code>
    /// </summary>
    internal abstract void CompareMax(T value);
    /// <summary>
    /// <code>
    /// when a new-value is added to the relation this axis belongs to, 
    /// the default min/max values will be recalculated only if new-value is less than Min / more than Max
    /// - Min sets itself to next digit less than new-value, but only if its at least 1/10 of Max, else its 0
    ///     f.e.    new-value = 15 AND Max = 20 => Min = DefaultMin = 10
    ///             new-value = 15 AND Max = 200 => Min = DefaultMin = 0 (not 10)
    /// </code>
    /// </summary>
    internal abstract void CompareMin(T value);
}

public class NAxis(string name, string unit, Number min, Number max) : 
AxisBase<Number>(name, unit, min, max)
{
    public override void SetMax(Number max)
    {
        if (max < Min) return;
        Max = max;
    }

    public override void SetMin(Number min)
    {
        if (min > Max) return;
        Min = min;
    }

    internal override void CompareMax(Number value)
    {
        if (value > Max)
        {
            Number val = new(1);

            while (value > val)
                val *= 10;
            val = val == Number.One ? new(1) : val / 10;

            Max = new(0);
            while (value > Max)
                Max += val;

            DefaultMax = Max;
        }
    }

    internal override void CompareMin(Number value)
    {
        if (value < Min)
        {
            Number val;
            if (value < Number.Zero)
            {
                val = new(-1);

                while (value < val)
                    val *= 10;

                val = val == Number.MinusOne ? new(-1) : val / 10;

                Min = new(0);
                while (value < Min)
                    Min += val;
            }
            else
            {
                val = new(1);

                while (value > val)
                    val += 10;

                val = val == Number.MinusOne ? new(1) : val / 10;

                Min = new(0);
                while (value > Min + val)
                    Min += val;
            }

            if (Min < Number.Zero)
            {
                if ((-1) * Min < Max / 10)
                    Min = (-1) * (Max / 10);
            }
            else
            {
                if (Min < Max / 10)
                    Min = new(0);
            }

            DefaultMin = Min;
        }
    }
}