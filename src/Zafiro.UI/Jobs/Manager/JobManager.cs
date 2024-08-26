using System.Reactive.Disposables;
using DynamicData;

namespace Zafiro.UI.Jobs.Manager;

public class JobManager : IJobManager
{
    private readonly CompositeDisposable disposables = new();
    private readonly SourceCache<JobItem, string> tasks = new(x => x.Job.Id);

    public JobManager()
    {
        Tasks = tasks.Connect();
    }

    public IObservable<IChangeSet<JobItem, string>> Tasks { get; }

    public void Add(Job job, JobOptions options)
    {
        if (options.RemoveOnCompleted)
        {
            job.OnCompleted(uiTask => tasks.Remove(uiTask.Id)).DisposeWith(disposables);
        }

        if (options.RemoveOnStopped)
        {
            job.OnStopped(uiTask => tasks.Remove(uiTask.Id)).DisposeWith(disposables);
        }

        tasks.AddOrUpdate(new JobItem(job, options), new LambdaComparer<JobItem>((a, b) => Equals(a.Id, b.Id)));

        if (options.AutoStart)
        {
            job.Execute().DisposeWith(disposables);
        }
    }
}