using DynamicData;

namespace Zafiro.UI.TaskManager;

public static class TaskManagerExtension
{
    public static bool IsTransient(this TaskItem taskItem) => taskItem.Options.RemoveOnCompleted || taskItem.Options.RemoveOnCompleted;
    public static IObservable<IChangeSet<TaskItem, string>> Transient(this IObservable<IChangeSet<TaskItem, string>> tasks) => tasks.Filter(x => x.IsTransient());
    public static IObservable<IChangeSet<TaskItem, string>> Permanent(this IObservable<IChangeSet<TaskItem, string>> tasks) => tasks.Filter(x => !x.IsTransient());
}