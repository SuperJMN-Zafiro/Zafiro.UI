using System;
using System.Reactive.Linq;
using ReactiveUI;
using Zafiro.UI.TaskManager;

namespace Sample.Samples;

public class TasksSampleViewModel : ReactiveObject
{
    public TasksSampleViewModel()
    {
        var taskManager = new TaskManager();
        TaskManagerViewModel = new TaskManagerViewModel(taskManager);
        var execution = ExecutionFactory.From(Observable.Timer(TimeSpan.FromSeconds(5)), Observable.Return(new DoubleProgress(50d)));
        var task = new UITask("Task", "Task",  new object(), Observable.Never(""), execution);
        taskManager.Add(task, new TaskOptions(){  AutoStart = true, RemoveOnCompleted = true, RemoveOnStopped = false});
    }

    public TaskManagerViewModel TaskManagerViewModel { get; }
}
