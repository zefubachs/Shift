using LiteDB;
using Shift.Storage.LiteDB.Repositories;
using Shift.Storage.Repositories;

namespace Shift.Storage.LiteDB;
public sealed class LiteDBSession(LiteDatabase database) : IStorageSession
{
    public IProjectRepository Projects => new ProjectRepository(database);
    public IFrameRepository Frames => new FrameRepository(database);

    public void Dispose()
    {
        database.Dispose();
    }
}
