namespace Zafiro.Avalonia.Models;

public interface ISection
{
    public string Title { get; }
    public object Icon { get; }
    public object Content { get; }
}