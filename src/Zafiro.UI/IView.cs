using System.Reactive;

namespace Zafiro.UI
{
    public interface IView : IContextualizable
    {
        void Close();
        Task ShowAsModal();
        string Title { get; set; }
        IObservable<Unit> Shown { get; }
    }
}