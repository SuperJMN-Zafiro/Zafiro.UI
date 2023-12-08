using System.Reactive;
using System.Windows.Input;
using ReactiveUI;

namespace Zafiro.UI;

public interface IStoppableCommand
{
    IObservable<bool> IsExecuting { get; }
    IObservable<bool> CanExecute { get; }
    public ICommand Start { get; }
    public ICommand Stop { get; }
}

public interface IStoppableCommand<TIn, TOut>
{
    IObservable<bool> IsExecuting { get; }
    public ReactiveCommand<TIn, TOut> Start { get; }
    public ReactiveCommandBase<Unit, Unit> Stop { get; }
}