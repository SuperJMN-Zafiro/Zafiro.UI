namespace Zafiro.UI.Jobs.Progress;

public class Proportion : IProgress
{
    public Proportion(double ratio)
    {
        Ratio = ratio;
    }

    public double Ratio { get; }
}