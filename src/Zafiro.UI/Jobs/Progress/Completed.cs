namespace Zafiro.UI.Jobs.Progress;

public class Completed : IProgress
{
    public static Completed Instance { get; } = new();
}