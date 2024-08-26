using Zafiro.Actions;
using Zafiro.Reactive;

namespace Zafiro.UI.TaskManager;

public abstract class ExecutionFactory
{
    public static IExecution From<T>(IObservable<T> observable, IObservable<IMyProgress> progress)
    {
        return new Execution(observable.ToSignal(), progress);
    }
}