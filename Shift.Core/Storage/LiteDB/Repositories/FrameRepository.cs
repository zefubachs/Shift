using LiteDB;
using Shift.Storage.LiteDB.Entities;
using Shift.Storage.Models;
using Shift.Storage.Repositories;

namespace Shift.Storage.LiteDB.Repositories;
public class FrameRepository : IFrameRepository
{
    private const string COLLECTION_NAME = "frames";

    private readonly ILiteCollection<FrameEntity> collection;

    public FrameRepository(LiteDatabase database)
    {
        collection = database.GetCollection<FrameEntity>(COLLECTION_NAME);
        collection.EnsureIndex(x => x.End);
    }

    public Task<Frame?> FindByIdAsync(string id)
    {
        var entity = collection.FindById(new ObjectId(id));
        if (entity is null)
            return Task.FromResult<Frame?>(null);

        return entity.AsModel().AsNullableTask();
    }

    public Task<Frame?> FindActiveAsync()
    {
        var entity = collection.FindOne(x => x.End == null);
        if (entity is null)
            return Task.FromResult<Frame?>(null);

        return entity.AsModel().AsNullableTask();
    }

    public Task AddAsync(Frame frame)
    {
        var entity = new FrameEntity
        {
            Project = frame.Project,
            Start = frame.Start,
            End = frame.End,
            Tags = frame.Tags,
        };
        collection.Insert(entity);
        frame.Id = entity.Id.ToString();
        return Task.CompletedTask;
    }

    public Task<bool> SetEndAsync(string id, DateTime dateTime)
    {
        var entity = collection.FindById(new ObjectId(id));
        if (entity is null)
            return Task.FromResult(false);

        entity.End = dateTime;
        collection.Update(entity);
        return Task.FromResult(true);
    }
}
