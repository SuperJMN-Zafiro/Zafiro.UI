using System.Windows.Input;

namespace Zafiro.UI;

public interface IStoppableCommand
{
    IObservable<bool> IsExecuting { get; }
    IObservable<bool> CanExecute { get; }
    public ICommand Start { get; }
    public ICommand Stop { get; }
}