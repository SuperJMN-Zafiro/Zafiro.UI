namespace Zafiro.UI.Jobs.Manager;

public class JobOptions
{
    public bool AutoStart { get; init; }
    public bool RemoveOnCompleted { get; init; }
    public bool RemoveOnStopped { get; init; }
}