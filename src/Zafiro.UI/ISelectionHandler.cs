using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;

namespace Zafiro.UI;

public interface ISelectionHandler
{
    ReactiveCommand<Unit, Unit> SelectNone { get; }
    ReactiveCommand<Unit, Unit> SelectAll { get; }
    IObservable<int> SelectionCount { get; }
    IObservable<int> TotalCount { get; }
}

public static class SelectionHandlerMixin
{
    public static IObservable<SelectionKind> Kind(this ISelectionHandler selectionHandler)
    {
        return selectionHandler.SelectionCount.WithLatestFrom(selectionHandler.TotalCount, (selected, total) => selected == 0 ? UI.SelectionKind.None : total == selected ? UI.SelectionKind.Full : UI.SelectionKind.Partial);
    }
}

public interface ISelectionHandler<T, TKey> : ISelectionHandler where T : notnull where TKey : notnull
{
    IObservable<IChangeSet<T, TKey>> SelectionChanges { get; }
}