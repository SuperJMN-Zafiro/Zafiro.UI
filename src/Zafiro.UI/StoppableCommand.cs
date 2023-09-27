using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using ReactiveUI;

namespace Zafiro.UI;

public static class StoppableCommand
{
    public static StoppableCommand<TIn, TOut> Create<TIn, TOut>(Func<TIn, IObservable<TOut>> logic, IObservable<bool> canStart)
    {
        return new StoppableCommand<TIn, TOut>(logic, canStart);
    }

    public static StoppableCommand<Unit, TOut> Create<TOut>(Func<IObservable<TOut>> logic, IObservable<bool> canStart)
    {
        return new StoppableCommand<Unit, TOut>(_ => logic(), canStart);
    }

    public static IStoppableCommand<Unit, TOut> CreateFromTask<TOut>(Func<CancellationToken, Task<TOut>> task, IObservable<bool> canExecute)
    {
        return new StoppableCommand<Unit, TOut>(_ => Observable.FromAsync(task), canExecute);
    }
}

public class StoppableCommand<TIn, TOut> : IStoppableCommand<TIn, TOut>, IStoppableCommand
{
    private readonly ReactiveCommand<TIn, TOut> startCommand;

    public StoppableCommand(Func<TIn, IObservable<TOut>> logic, IObservable<bool> canStart)
    {
        var isExecuting = new Subject<bool>();
        var stopCommand = ReactiveCommand.Create(() => { }, isExecuting);
        Stop = stopCommand;
        startCommand = ReactiveCommand.CreateFromObservable<TIn, TOut>(e => logic(e).TakeUntil(stopCommand), canStart);
        Start = startCommand;
        startCommand.IsExecuting.Subscribe(isExecuting);
    }

    public ReactiveCommand<TIn, TOut> Start { get; }
    IReactiveCommand IStoppableCommand.Stop => Stop;

    IReactiveCommand IStoppableCommand.Start => Start;
    public ReactiveCommand<Unit, Unit> Stop { get; }

    public IObservable<bool> IsExecuting => startCommand.IsExecuting;
    public IObservable<bool> CanExecute => startCommand.CanExecute;
    public IObservable<TOut> Results => startCommand;
}