namespace Shift.Storage.Models;
public class Project
{
    public string Id { get; set; } = default!;
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool Active { get; set; } = true;
}
