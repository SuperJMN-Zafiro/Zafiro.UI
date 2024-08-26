using System.Collections.ObjectModel;

namespace Zafiro.UI.Jobs;

public interface IJobListings
{
    ReadOnlyObservableCollection<Job> Tasks { get; }
    ReadOnlyObservableCollection<Job> PermanentTasks { get; }
    ReadOnlyObservableCollection<Job> TransientTasks { get; }
}