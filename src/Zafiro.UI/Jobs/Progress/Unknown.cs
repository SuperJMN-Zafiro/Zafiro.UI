namespace Zafiro.UI.Jobs.Progress;

public class Unknown : IProgress
{
    public static Unknown Instance { get; } = new();
}