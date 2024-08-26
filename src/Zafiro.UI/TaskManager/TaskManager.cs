using System.Reactive.Disposables;
using DynamicData;

namespace Zafiro.UI.TaskManager;

public class TaskManager
{
    private readonly SourceCache<TaskItem, string> tasks = new(x => x.Task.Id);
    private readonly CompositeDisposable disposables = new();

    public TaskManager()
    {
        Tasks = tasks.Connect();
    }

    public IObservable<IChangeSet<TaskItem, string>> Tasks { get; }

    public void Add(UITask task, TaskOptions options)
    {
        if (options.RemoveOnCompleted)
        {
            task.OnCompleted(uiTask => tasks.Remove(uiTask.Id)).DisposeWith(disposables);
        }

        if (options.RemoveOnStopped)
        {
            task.OnStopped(uiTask => tasks.Remove(uiTask.Id)).DisposeWith(disposables);
        }

        tasks.AddOrUpdate(new TaskItem(task, options), new LambdaComparer<TaskItem>((a, b) => Equals(a.Id, b.Id)));

        if (options.AutoStart)
        {
            task.Execution.Start.Subscribe().DisposeWith(disposables);
        }
    }
}