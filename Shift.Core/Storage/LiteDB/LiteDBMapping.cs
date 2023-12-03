using Shift.Storage.LiteDB.Entities;
using Shift.Storage.Models;

namespace Shift.Storage.LiteDB;
public static class LiteDBMapping
{
    public static Task<T> AsTask<T>(this T source)
        => Task.FromResult(source);

    public static Task<T?> AsNullableTask<T>(this T source)
        => Task.FromResult<T?>(source);

    public static Frame AsModel(this FrameEntity entity) => new Frame
    {
        Id = entity.Id.ToString(),
        Project = entity.Project,
        Start = entity.Start,
        End = entity.End,
        Tags = entity.Tags,
    };
}
