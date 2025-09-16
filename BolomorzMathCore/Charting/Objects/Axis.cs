namespace BolomorzMathCore.Charting;

public class Axis(string name, string unit, double min, double max)
{
    public string Name { get; private set; } = name;
    public string Unit { get; private set; } = unit;
    public double LastVal { get; private set; } = double.MinValue;
    public double Min { get; private set; } = min;
    public double Max { get; private set; } = max;
    public double DefaultMin { get; private set; } = min;
    public double DefaultMax { get; private set; } = max;

    public void SetMin(double min)
    {
        if (min > Max) return;
        Min = min;
    }
    public void SetMax(double max)
    {
        if (max < Min) return;
        Max = max;
    }
    public void ResetMin()
    {
        Min = DefaultMin;
    }
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