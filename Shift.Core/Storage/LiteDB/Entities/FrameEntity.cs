using LiteDB;

namespace Shift.Storage.LiteDB.Entities;
public class FrameEntity
{
    public ObjectId Id { get; set; } = default!;
    public required string Project { get; set; }
    public required DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public ISet<string> Tags { get; set; } = new HashSet<string>();
}
