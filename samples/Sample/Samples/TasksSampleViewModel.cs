using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
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
        JobListings = new JobListings(taskManager);

        taskManager.Add(CreateJob("Task1", "Task 1"), new JobOptions { AutoStart = false, RemoveOnCompleted = false, RemoveOnStopped = true });
        taskManager.Add(CreateJob("Task2", "Task 2"), new JobOptions { AutoStart = false, RemoveOnCompleted = false, RemoveOnStopped = true });
        taskManager.Add(CreateJob("Task3", "Task 3"), new JobOptions { AutoStart = false, RemoveOnCompleted = false, RemoveOnStopped = false });
    }

    private static Job CreateJob(string id, string name)
    {
        var subject = new BehaviorSubject<IProgress>(new Unknown());
        var execution = ExecutionFactory.From(async token =>
        {
            subject.OnNext(new Unknown());
            await Task.Delay(2000, token);
            subject.OnNext(new Proportion(0.3));
            await Task.Delay(1000, token);
            subject.OnNext(new Proportion(0.6));
            await Task.Delay(2000, token);
            subject.OnNext(new Proportion(0.8));
            await Task.Delay(1500, token);
            subject.OnNext(new Completed());
            await Task.Delay(1000, token);
        }, subject);
        var task = new Job(id, name, new Icon(), Observable.Never(""), execution);
        return task;
    }

    public JobListings JobListings { get; }
}

public class Icon
{
}