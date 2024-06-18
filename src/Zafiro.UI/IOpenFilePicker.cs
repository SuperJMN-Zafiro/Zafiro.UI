using CSharpFunctionalExtensions;
using Zafiro.FileSystem.Core;

namespace Zafiro.UI;

public interface IOpenFilePicker
{
    IObservable<IEnumerable<IZafiroFile>> PickMultiple(params FileTypeFilter[] filters);
    IObservable<Maybe<IZafiroFile>> PickSingle(params FileTypeFilter[] filters);
}