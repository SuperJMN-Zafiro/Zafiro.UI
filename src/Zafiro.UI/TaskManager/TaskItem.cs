namespace Zafiro.UI.TaskManager;

public class TaskItem(IUITask task, TaskOptions options)
{
    public string Id => Task.Id;
    public IUITask Task { get; } = task;
    public TaskOptions Options { get;  } = options;
}