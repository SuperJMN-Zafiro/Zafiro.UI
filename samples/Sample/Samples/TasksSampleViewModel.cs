using System;
using System.Reactive;
using System.Reactive.Linq;
using CSharpFunctionalExtensions;
using ReactiveUI;
using Zafiro.Reactive;
using Zafiro.UI;
using Zafiro.UI.TaskManager;

namespace Sample.Samples;

public class TasksSampleViewModel : ReactiveObject
{
    public TasksSampleViewModel()
    {
        var taskManager = new TaskManager();
        this.TaskManagerViewModel = new TaskManagerViewModel(taskManager);
        var unitTask = new MyTask("Task1", "My task", StoppableCommand.Create(() => Observable.Timer(TimeSpan.FromSeconds(5)).ToSignal(), Maybe<IObservable<bool>>.None));
        taskManager.Add(unitTask, new TaskOptions(){  AutoStart = true, RemoveOnCompleted = true, RemoveOnStopped = false});
    }

    public TaskManagerViewModel TaskManagerViewModel { get; }
}

public class MyTask : IUnitTask
{
    public MyTask(string id, string name, IStoppableCommand<Unit, Unit> execution)
    {
        Id = id;
        Name = name;
        Execution = execution;
        ReactiveExecution = execution;
    }

    public IStoppableCommand Execution { get; }
    public string Id { get; }
    public string Name { get; set; }
    public IStoppableCommand<Unit, Unit> ReactiveExecution { get; }
}