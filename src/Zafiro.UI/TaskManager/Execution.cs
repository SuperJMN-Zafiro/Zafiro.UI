using System.Reactive;
using CSharpFunctionalExtensions;
using ReactiveUI;
using Zafiro.Reactive;

namespace Zafiro.UI.TaskManager;

public class Execution : IExecution
{
    public Execution(IObservable<Unit> observable, IObservable<IMyProgress> progress)
    {
        Progress = progress;
        var stoppable = StoppableCommand.Create(observable.ToSignal, Maybe<IObservable<bool>>.None);
        Start = stoppable.StartReactive;
        Stop = stoppable.StopReactive;
    }

    public ReactiveCommandBase<Unit, Unit> Start { get; }
    public ReactiveCommandBase<Unit, Unit> Stop { get; }
    public IObservable<IMyProgress> Progress { get; set; }
}