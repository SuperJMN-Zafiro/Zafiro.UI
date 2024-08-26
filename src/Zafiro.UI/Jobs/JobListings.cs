using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using DynamicData;
using Zafiro.UI.Jobs.Manager;

namespace Zafiro.UI.Jobs;

public class JobListings : IDisposable, IJobListings
{
    private readonly CompositeDisposable compositeDisposable = new();

    public JobListings(JobManager jobManager)
    {
        jobManager.Tasks.Transient().Transform(x => x.Job).Bind(out var transient).Subscribe().DisposeWith(compositeDisposable);
        TransientTasks = transient;
        jobManager.Tasks.Permanent().Transform(x => x.Job).Bind(out var permanent).Subscribe().DisposeWith(compositeDisposable);
        PermanentTasks = permanent;
        jobManager.Tasks.Transform(x => x.Job).Bind(out var tasks).Subscribe().DisposeWith(compositeDisposable);
        Tasks = tasks;
    }

    public ReadOnlyObservableCollection<Job> Tasks { get; }

    public ReadOnlyObservableCollection<Job> PermanentTasks { get; }

    public ReadOnlyObservableCollection<Job> TransientTasks { get; }

    public void Dispose()
    {
        compositeDisposable.Dispose();
    }
}