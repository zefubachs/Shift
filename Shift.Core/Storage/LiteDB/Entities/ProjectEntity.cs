using LiteDB;

namespace Shift.Storage.LiteDB.Entities;
public class ProjectEntity
{
    public ObjectId Id { get; set; } = default!;
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; }
}
