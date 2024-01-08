﻿using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Helpers;
using Zafiro.Reactive;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Zafiro.UI.Fields;

public class Field<T> : ReactiveValidationObject, IField
{
    public Field()
    {
        this.WhenAnyValue(x => x.CommittedValue).BindTo(this, x => x.Value);
        this.WhenAnyValue(x => x.Initial).Do(v => CommittedValue = v).Subscribe();
        Commit = ReactiveCommand.Create(() =>
        {
            CommittedValue = Value!;
            return Unit.Default;
        }, IsValid);
        IsDirty = this.WhenAnyValue(x => x.CommittedValue, x => x.Value, (cv, v) => !Equals(cv, v));
        Rollback = ReactiveCommand.Create(() =>
        {
            Value = CommittedValue!;
        });
    }

    [Reactive]
    public T CommittedValue { get; private set; }

    [Reactive]
    public T Value { get; set; }

    [Reactive]
    public T Initial { get; set; }

    public ReactiveCommandBase<Unit, Unit> Commit { get; }
    public ReactiveCommandBase<Unit, Unit> Rollback { get; }
    public IObservable<bool> IsValid => ValidationContext.Valid;
    public IObservable<bool> IsDirty { get; }
}