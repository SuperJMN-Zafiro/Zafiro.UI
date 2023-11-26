using ReactiveUI;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;
using Zafiro.Mixins;

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
}