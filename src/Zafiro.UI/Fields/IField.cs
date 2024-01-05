namespace Zafiro.UI.Fields;

public interface IField : IValidatable
{
    public IObservable<bool> IsDirty { get; }
}