using System.Reactive.Linq;

namespace Zafiro.UI;

public static class SelectionHandlerMixin
{
    public static IObservable<SelectionKind> Kind(this ISelectionHandler selectionHandler)
    {
        return selectionHandler.SelectionCount.WithLatestFrom(selectionHandler.TotalCount, (selected, total) => selected == 0 ? UI.SelectionKind.None : total == selected ? UI.SelectionKind.Full : UI.SelectionKind.Partial);
    }
}