using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Zafiro.UI.TaskManager;

public static class TaskExtensions
{
    public static IDisposable Execute(this UITask task)
    {
        return task.Execution.Start.Execute().Subscribe();
    }

    public static IDisposable OnCompleted(this UITask task, Action<UITask> onFinished)
    {
        return task
            .Execution.Start
            .Do(_ => onFinished(task))
            .Subscribe();
    }

    public static IDisposable OnStopped(this UITask task, Action<UITask> onFinished)
    {
        return task.
            Execution.Stop
            .Do(_ => onFinished(task))
            .Subscribe();
    }
}