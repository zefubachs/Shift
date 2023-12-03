using Shift.Storage.Repositories;

namespace Shift.Storage;
public interface IStorageSession : IDisposable
{
    IProjectRepository Projects { get; }
    IFrameRepository Frames { get; }
}
