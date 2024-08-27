namespace Zafiro.UI.Jobs.Progress;

public class None : IProgress
{
    public static None Instance { get; } = new();
}