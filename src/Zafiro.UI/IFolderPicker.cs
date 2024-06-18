using CSharpFunctionalExtensions;
using Zafiro.FileSystem.Core;

namespace Zafiro.UI;

public interface IFolderPicker
{
    IObservable<Maybe<IZafiroDirectory>> Pick(string title);
}