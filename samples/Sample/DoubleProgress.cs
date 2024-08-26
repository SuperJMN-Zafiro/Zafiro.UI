using Zafiro.Actions;
using Zafiro.UI.TaskManager;

namespace Sample;

public class DoubleProgress : IMyProgress
{
    public DoubleProgress(double value)
    {
        Value = value;
    }

    public double Value { get; }
}