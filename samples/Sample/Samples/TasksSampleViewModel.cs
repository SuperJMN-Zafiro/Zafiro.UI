using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.PLinq;
using ReactiveUI;
using Zafiro.UI.Jobs;
using Zafiro.UI.Jobs.Execution;
using Zafiro.UI.Jobs.Manager;
using Zafiro.UI.Jobs.Progress;

namespace Sample.Samples;

public class TasksSampleViewModel : ReactiveObject
{
    public TasksSampleViewModel()
    {
        var taskManager = new JobManager();

        taskManager.Add(CreateStoppableJob("Task1", "Task 1"), new JobOptions { AutoStart = false, RemoveOnCompleted = false, RemoveOnStopped = true });
        taskManager.Add(CreateStoppableJob("Task2", "Task 2"), new JobOptions { AutoStart = false, RemoveOnCompleted = false, RemoveOnStopped = true });
        taskManager.Add(CreateStoppableJob("Task3", "Task 3"), new JobOptions { AutoStart = false, RemoveOnCompleted = false, RemoveOnStopped = false });
        taskManager.Add(CreateUnstoppableJob("Task4", "Task 4"), new JobOptions { AutoStart = false, RemoveOnCompleted = false, RemoveOnStopped = false });

        taskManager.Tasks.Permanent().Transform(x => x.Job).Bind(out var permanentJobs).Subscribe();
        PermanentJobs = permanentJobs;
    }

    public ReadOnlyObservableCollection<Job> PermanentJobs { get; }

    private static Job CreateStoppableJob(string id, string name)
    {
        var subject = new BehaviorSubject<IProgress>(new Unknown());
        var execution = ExecutionFactory.From(async ct =>
        {
            subject.OnNext(new Unknown());
            await Task.Delay(2000, ct);
            subject.OnNext(new Proportion(0.3));
            await Task.Delay(1000, ct);
            subject.OnNext(new Proportion(0.6));
            await Task.Delay(2000, ct);
            subject.OnNext(new Proportion(0.8));
            await Task.Delay(1500, ct);
            subject.OnNext(new Completed());
            await Task.Delay(1000, ct);
        }, subject);
        var task = new Job(id, name, new Icon(), Observable.Never(""), execution);
        return task;
    }
    
    private static Job CreateUnstoppableJob(string id, string name)
    {
        var subject = new BehaviorSubject<IProgress>(new Unknown());
        var execution = ExecutionFactory.From(async () =>
        {
            subject.OnNext(new Unknown());
            await Task.Delay(2000);
            subject.OnNext(new Proportion(0.3));
            await Task.Delay(1000);
            subject.OnNext(new Proportion(0.6));
            await Task.Delay(2000);
            subject.OnNext(new Proportion(0.8));
            await Task.Delay(1500);
            subject.OnNext(new Completed());
            await Task.Delay(1000);
        }, subject);
        var task = new Job(id, name, new Icon(), Observable.Never(""), execution);
        return task;
    }
}

public class Icon
{
}