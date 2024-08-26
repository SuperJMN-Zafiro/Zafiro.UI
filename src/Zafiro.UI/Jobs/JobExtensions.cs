using System.Reactive.Linq;

namespace Zafiro.UI.Jobs;

public static class JobExtensions
{
    public static IDisposable Execute(this Job task)
    {
        return task.Execution.Start.Execute().Subscribe();
    }

    public static IDisposable OnCompleted(this Job task, Action<Job> onFinished)
    {
        return task
            .Execution.Start
            .Do(_ => onFinished(task))
            .Subscribe();
    }

    public static IDisposable OnStopped(this Job task, Action<Job> onFinished)
    {
        return task.Execution.Stop
            .Do(_ => onFinished(task))
            .Subscribe();
    }
}