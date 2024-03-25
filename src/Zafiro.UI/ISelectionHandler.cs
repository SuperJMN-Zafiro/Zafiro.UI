using System.Reactive;
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

public interface ISelectionHandler<T, TKey> : ISelectionHandler where T : notnull where TKey : notnull
{
    IObservable<IChangeSet<T, TKey>> SelectionChanges { get; }
}