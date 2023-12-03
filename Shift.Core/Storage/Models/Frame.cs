namespace Shift.Storage.Models;
public class Frame
{
    public string Id { get; set; } = default!;
    public required string Project { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public ISet<string> Tags { get; set; } = new HashSet<string>();
}
