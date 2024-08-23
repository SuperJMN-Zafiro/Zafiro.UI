using Sample.Samples;
using Zafiro.UI.Fields;

namespace Sample.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public Field<string> Name { get; }

    public TasksSampleViewModel TasksSampleViewModel { get; set; } = new TasksSampleViewModel();

    public MainViewModel()
    {
        Name = new Field<string>("Saludos");
        Name.Validate(s => s.Contains("Sal"), "Doesn't contain sal");
        Name.AutoCommit();
    }
}
