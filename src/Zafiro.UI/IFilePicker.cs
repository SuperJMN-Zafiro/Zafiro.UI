using CSharpFunctionalExtensions;
using Zafiro.FileSystem;
using Zafiro.FileSystem.Mutable;

namespace Zafiro.UI;

public interface IFilePicker
{
    IObservable<IEnumerable<IZafiroFile>> PickForOpenMultiple(params FileTypeFilter[] filters);
    IObservable<Maybe<IZafiroFile>> PickForOpen(params FileTypeFilter[] filters);
    Task<Maybe<IMutableFile>> PickForSave(string desiredName, Maybe<string> defaultExtension, params FileTypeFilter[] filters);
}