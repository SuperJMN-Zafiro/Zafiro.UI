namespace Zafiro.UI.Jobs.Manager;

public class JobItem(Job job, JobOptions options)
{
    public string Id => Job.Id;
    public Job Job { get; } = job;
    public JobOptions Options { get; } = options;
}