namespace Zafiro.UI.TaskManager;

public class TaskItem(UITask task, TaskOptions options)
{
    public string Id => Task.Id;
    public UITask Task { get; } = task;
    public TaskOptions Options { get;  } = options;
}