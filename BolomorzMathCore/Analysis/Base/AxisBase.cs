using BolomorzMathCore.Basics;

namespace BolomorzMathCore.Analysis;

public abstract class AxisBase<T>(string name, string unit, T min, T max) where T : class
{
    public string Name { get; set; } = name;
    public string Unit { get; set; } = unit;
    public T Min { get; protected set; } = min;
    public T Max { get; protected set; } = max;
    protected T DefaultMin = min;
    protected T DefaultMax = max;

    public abstract void SetMin(T min);
    public abstract void SetMax(T max);
    public void ResetMin()
    {
        Min = DefaultMin;
    }
    public void ResetMax()
    {
        Max = DefaultMax;
    }

    internal abstract void CompareMax(T value);
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