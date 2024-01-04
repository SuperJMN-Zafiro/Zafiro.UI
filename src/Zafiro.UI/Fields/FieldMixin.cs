using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using Zafiro.Reactive;

namespace Zafiro.UI.Fields;

public static class FieldMixin
{
    public static ValidationHelper AddRule<T>(this Field<T> field, Func<T, bool> isPropertyValid, string errorMessage)
    {
        return field.ValidationRule(x => x.Value, isPropertyValid!, errorMessage);
    }

    public static ValidationHelper AddRule<T>(this Field<T> field, IObservable<bool> isPropertyValid, string errorMessage)
    {
        return field.ValidationRule(x => x.Value, isPropertyValid, errorMessage);
    }

    public static IDisposable AutoCommit<T>(this Field<T> field)
    {
        return field.WhenAnyValue(x => x.Value).ToSignal().InvokeCommand(field.Commit);
    }

    public static IDisposable InvokeCommand<T, TResult>(this IObservable<T> item, IReactiveCommand<T, TResult>? command) =>
        command is null
            ? throw new ArgumentNullException(nameof(command))
            : WithLatestFromFixed(item, command.CanExecute, (value, canExecute) => new InvokeCommandInfo<IReactiveCommand<T, TResult>, T>(command, canExecute, value))
                .Where(ii => ii.CanExecute)
                .SelectMany(ii => command.Execute(ii.Value).Catch(Observable.Empty<TResult>()))
                .Subscribe();

    private readonly struct InvokeCommandInfo<TCommand, TValue>
    {
        public InvokeCommandInfo(TCommand command, bool canExecute, TValue value)
        {
            Command = command;
            CanExecute = canExecute;
            Value = value!;
        }

        public TCommand Command { get; }

        public bool CanExecute { get; }

        public TValue Value { get; }

        public InvokeCommandInfo<TCommand, TValue> WithValue(TValue value) =>
            new(Command, CanExecute, value);
    }

    // See https://github.com/Reactive-Extensions/Rx.NET/issues/444
    private static IObservable<TResult> WithLatestFromFixed<TLeft, TRight, TResult>(
        IObservable<TLeft> item,
        IObservable<TRight> other,
        Func<TLeft, TRight, TResult> resultSelector) =>
        item
            .Publish(
                os =>
                    other
                        .Select(
                            a =>
                                os
                                    .Select(b => resultSelector(b, a)))
                        .Switch());
}