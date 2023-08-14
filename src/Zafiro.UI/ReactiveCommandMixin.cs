using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloniaSyncer.ViewModels;

public static class ReactiveCommandMixin
{
    public static ReactiveCommand<TInput, TResult> Extend<TInput, TResult>(this ReactiveCommand<TInput, TResult> command, Action<TResult> afterExecution)
    {
        return ReactiveCommand.CreateFromTask<TInput, TResult>(async input =>
        {
            var firstAsync = await command.Execute(input).FirstAsync();
            afterExecution(firstAsync);
            return firstAsync;
        }, command.CanExecute);
    }
}