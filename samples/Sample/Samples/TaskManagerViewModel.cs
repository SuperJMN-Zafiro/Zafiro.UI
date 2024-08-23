using System;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using DynamicData;
using Zafiro.UI.TaskManager;

namespace Sample.Samples;

public class TaskManagerViewModel : IDisposable
{
    private readonly CompositeDisposable compositeDisposable = new CompositeDisposable();

    public TaskManagerViewModel(TaskManager taskManager)
    {
        taskManager.Tasks.Transient().Transform(x => x.Task).Bind(out var transient).Subscribe().DisposeWith(compositeDisposable);
        TransientTasks = transient;
        taskManager.Tasks.Permanent().Transform(x => x.Task).Bind(out var permanent).Subscribe().DisposeWith(compositeDisposable);
        PermanentTasks = permanent;
        taskManager.Tasks.Transform(x => x.Task).Bind(out var tasks).Subscribe().DisposeWith(compositeDisposable);
        Tasks = tasks;
    }

    public ReadOnlyObservableCollection<IUITask> Tasks { get; }

    public ReadOnlyObservableCollection<IUITask> PermanentTasks { get; }

    public ReadOnlyObservableCollection<IUITask> TransientTasks { get; }

    public void Dispose()
    {
        compositeDisposable.Dispose();
    }
}