using System.Reactive;
using CSharpFunctionalExtensions;
using ReactiveUI;
using Zafiro.Reactive;
using Zafiro.UI.Jobs.Progress;

namespace Zafiro.UI.Jobs.Execution;

public class StoppableExecution : IExecution
{
    public StoppableExecution(IObservable<Unit> observable, IObservable<IProgress> progress)
    {
        Progress = progress;
        var stoppable = StoppableCommand.Create(observable.ToSignal, Maybe<IObservable<bool>>.None);
        Start = stoppable.StartReactive;
        Stop = stoppable.StopReactive;
    }

    public ReactiveCommandBase<Unit, Unit> Start { get; }
    public ReactiveCommandBase<Unit, Unit> Stop { get; }
    public IObservable<IProgress> Progress { get; }
}