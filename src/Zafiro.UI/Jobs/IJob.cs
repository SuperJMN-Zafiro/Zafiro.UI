using Zafiro.UI.Jobs.Execution;

namespace Zafiro.UI.Jobs;

public interface IJob
{
    string Id { get; }
    string Name { get; }
    object Icon { get; }
    IObservable<string> Status { get; }
    IExecution Execution { get; }
}