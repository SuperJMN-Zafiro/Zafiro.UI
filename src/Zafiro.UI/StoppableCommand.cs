using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using CSharpFunctionalExtensions;
using ReactiveUI;

namespace Zafiro.UI;

public static class StoppableCommand
{
    public static StoppableCommand<TIn, TOut> Create<TIn, TOut>(Func<TIn, IObservable<TOut>> logic, Maybe<IObservable<bool>> canStart) => new(logic, canStart);

    public static StoppableCommand<Unit, TOut> Create<TOut>(Func<IObservable<TOut>> logic, Maybe<IObservable<bool>> canStart)
    {
        return new StoppableCommand<Unit, TOut>(_ => logic(), canStart);
    }

    public static IStoppableCommand<Unit, TOut> CreateFromTask<TOut>(Func<CancellationToken, Task<TOut>> task, Maybe<IObservable<bool>> canStart)
    {
        return new StoppableCommand<Unit, TOut>(_ => Observable.FromAsync(task), canStart);
    }
}

public class StoppableCommand<TIn, TOut> : IStoppableCommand<TIn, TOut>, IStoppableCommand
{
    public StoppableCommand(Func<TIn, IObservable<TOut>> logic, Maybe<IObservable<bool>> canStart)
    {
        var isExecuting = new Subject<bool>();
        Stop = ReactiveCommand.Create(() => { }, isExecuting);
        Start = ReactiveCommand.CreateFromObservable<TIn, TOut>(e => logic(e).TakeUntil(Stop), canStart.GetValueOrDefault());
        Start.IsExecuting.Subscribe(isExecuting);
    }

    ICommand IStoppableCommand.Start => Start;
    ICommand IStoppableCommand.Stop => Stop;
    public IObservable<bool> CanExecute => Start.CanExecute;
    public ReactiveCommandBase<Unit, Unit> Stop { get; }
    public ReactiveCommand<TIn, TOut> Start { get; }
    public IObservable<bool> IsExecuting => Start.IsExecuting;
}