using System.Reactive;
using ReactiveUI;

namespace Zafiro.UI.TaskManager;

public interface IExecution
{
    public ReactiveCommandBase<Unit, Unit> Start { get; }
    public ReactiveCommandBase<Unit, Unit> Stop { get; }
    public IObservable<IMyProgress> Progress { get; set; }
}

public interface IMyProgress;

public class UnknownProgress : IMyProgress;
public class Completed : IMyProgress;