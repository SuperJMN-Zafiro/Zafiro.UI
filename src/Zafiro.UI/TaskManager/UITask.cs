namespace Zafiro.UI.TaskManager;

public class UITask
{
    public UITask(string id, string name, object icon, IObservable<string> status, IExecution execution)
    {
        Id = id;
        Name = name;
        Icon = icon;
        Status = status;
        Execution = execution;
    }

    public string Id { get; }
    public string Name { get;  }
    public object Icon { get; }
    public IObservable<string> Status { get; }
    public IExecution Execution { get; }
}