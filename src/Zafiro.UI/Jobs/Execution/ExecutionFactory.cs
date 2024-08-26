using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Zafiro.Reactive;
using Zafiro.UI.Jobs.Progress;

namespace Zafiro.UI.Jobs.Execution;

public abstract class ExecutionFactory
{
    public static IExecution From<T>(IObservable<T> onExecute, IObservable<IProgress> progress)
    {
        return new StoppableExecution(onExecute.ToSignal(), progress);
    }

    public static IExecution From(Func<CancellationToken, Task> taskFactory, IObservable<IProgress> progress)
    {
        return new StoppableExecution(Observable.FromAsync(taskFactory).ToSignal(), progress);
    }
    
    public static IExecution From(Func<Task> taskFactory, IObservable<IProgress> progress)
    {
        return new UnstoppableExecution(Observable.FromAsync(taskFactory).ToSignal(), progress);
    }
    
    public static IExecution From<T>(Func<CancellationToken, Task<T>> taskFactory, IObservable<IProgress> progress)
    {
        return new StoppableExecution(Observable.FromAsync(taskFactory).ToSignal(), progress);
    }
    
    public static IExecution From<T>(ReactiveCommandBase<Unit, Unit> start, ReactiveCommandBase<Unit, Unit> stop, IObservable<IProgress> progress)
    {
        return new StartStopExecution(start, stop, progress);
    }
}