using System.Reactive.Linq;

namespace Zafiro.UI;

public static class SelectionHandlerMixin
{
    public static IObservable<SelectionKind> Kinds(this ISelectionHandler selectionHandler)
    {
        selectionHandler.SelectionCount.Subscribe(i => { });
        selectionHandler.TotalCount.Subscribe(i => { });
        
        return selectionHandler.SelectionCount.WithLatestFrom(selectionHandler.TotalCount.StartWith(0), (selected, total) => selected == 0 ? UI.SelectionKind.None : total == selected ? UI.SelectionKind.Full : UI.SelectionKind.Partial);
    }
}