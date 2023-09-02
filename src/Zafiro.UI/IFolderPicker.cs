using CSharpFunctionalExtensions;
using Zafiro.FileSystem;

namespace Zafiro.UI;

public interface IFolderPicker
{
    IObservable<Maybe<IZafiroDirectory>> Pick(string title);
}