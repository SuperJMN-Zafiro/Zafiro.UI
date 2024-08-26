using Zafiro.UI.Jobs.Execution;

namespace Zafiro.UI.Jobs;

public class Job
{
    public Job(string id, string name, object icon, IObservable<string> status, IExecution execution)
    {
        Id = id;
        Name = name;
        Icon = icon;
        Status = status;
        Execution = execution;
    }

    public string Id { get; }
    public string Name { get; }
    public object Icon { get; }
    public IObservable<string> Status { get; }
    public IExecution Execution { get; }
}