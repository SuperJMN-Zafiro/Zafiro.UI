using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.Validation.Helpers;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Zafiro.UI.Fields;

public class Field<T> : ReactiveValidationObject
{
    public Field(T initialValue)
    {
        CommittedValue = initialValue;
        this.WhenAnyValue(x => x.CommittedValue).BindTo(this, x => x.Value);
        Commit = ReactiveCommand.Create(() => CommittedValue = Value!, IsValid);
        Rollback = ReactiveCommand.Create(() => Value = CommittedValue);
    }

    [Reactive]
    public T CommittedValue { get; private set; }

    [Reactive]
    public T Value { get; set; }
    public ICommand Commit { get; }
    public ICommand Rollback { get; }
    public IObservable<bool> IsValid => ValidationContext.Valid;
}