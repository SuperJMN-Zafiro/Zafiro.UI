using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using CSharpFunctionalExtensions;
using DynamicData;
using ReactiveUI;

namespace Zafiro.UI.TaskManager;

public static class TaskExtensions
{
    public static IDisposable Execute(this IUnitTask task)
    {
        var compositeDisposable = new CompositeDisposable();
        return task.ReactiveExecution.StartReactive.Execute().Subscribe().DisposeWith(compositeDisposable);
    }

    public static IDisposable OnCompleted(this IUnitTask task, Action<IUnitTask> onFinished)
    {
        return task.ReactiveExecution.StartReactive
            .Do(_ => onFinished(task))
            .Subscribe();
    }

    public static IDisposable OnStopped(this IUnitTask task, Action<IUnitTask> onFinished)
    {
        return task.ReactiveExecution.StopReactive
            .Do(_ => onFinished(task))
            .Subscribe();
    }
}

public static class TaskManagerExtension
{
    public static bool IsTransient(this TaskItem taskItem) => taskItem.Options.RemoveOnCompleted || taskItem.Options.RemoveOnCompleted;
    public static IObservable<IChangeSet<TaskItem, string>> Transient(this IObservable<IChangeSet<TaskItem, string>> tasks) => tasks.Filter(x => x.IsTransient());
    public static IObservable<IChangeSet<TaskItem, string>> Permanent(this IObservable<IChangeSet<TaskItem, string>> tasks) => tasks.Filter(x => !x.IsTransient());
}

public class TaskOptions
{
    public bool AutoStart { get; set; }
    public bool RemoveOnCompleted { get; set; }
    public bool RemoveOnStopped { get; set; }
}

public class TaskManager
{
    private readonly SourceCache<TaskItem, string> tasks = new(x => x.Task.Id);
    private readonly CompositeDisposable disposables = new();

    public TaskManager()
    {
        Tasks = tasks.Connect();
    }

    public IObservable<IChangeSet<TaskItem, string>> Tasks { get; }

    public void Add(IUnitTask task, TaskOptions options)
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
            task.Execute().DisposeWith(disposables);
        }
    }
}

public interface IUITask
{
    IStoppableCommand Execution { get; }
    string Id { get; }
    string Name { get; set; }
}

public interface IUnitTask : IUITask<Unit, Unit>
{
}

public interface IUITask<TIn, TOut> : IUITask
{
    IStoppableCommand<TIn, TOut> ReactiveExecution { get; }
}