using System.Reactive;
using ReactiveUI;

namespace Zafiro.UI;

public interface IStoppableCommand
{
    IObservable<bool> IsExecuting { get; }
    IObservable<bool> CanExecute { get; }
    public IReactiveCommand Start { get; }
    public IReactiveCommand Stop { get; }
}

public interface IStoppableCommand<TIn, TOut>
{
    IObservable<bool> IsExecuting { get; }
    public ReactiveCommand<TIn, TOut> Start { get; }
    public ReactiveCommand<Unit, Unit> Stop { get; }
}