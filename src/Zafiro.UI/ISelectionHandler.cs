﻿using System.Reactive;
using DynamicData;
using ReactiveUI;

namespace Zafiro.UI;

public interface ISelectionHandler
{
    ReactiveCommand<Unit, Unit> SelectNone { get; }
    ReactiveCommand<Unit, Unit> SelectAll { get; }
    IObservable<SelectionKind> SelectionKind { get; }
    IObservable<int> SelectedItems { get; }
    IObservable<int> TotalItems { get; }
}

public interface ISelectionHandler<T, TKey> : ISelectionHandler where T : notnull where TKey : notnull
{
    IObservable<IChangeSet<T, TKey>> SelectionChanges { get; }
}