using System.Drawing;
using BolomorzMathCore.Visualization.Structs;

namespace BolomorzMathCore.Visualization;

public interface IBMCElement
{
    public abstract bool Equals(IBMCElement? other);
    public abstract void SetAttributes(Dictionary<string, object> attributes);
    public abstract bool IsPointInGeometry(BMCPoint point);
    public abstract bool QueryBy(Dictionary<string, object> query);
    public abstract BMCCollection GetBMCCollection();
    public abstract void Remove();
}