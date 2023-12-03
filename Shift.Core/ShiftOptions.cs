using System.Text.Json;

namespace Shift;
public class ShiftOptions
{
    public StoreSection Store { get; set; } = StoreSection.Default;

    public void SaveUserDefault()
    {
        var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "shift");
        Directory.CreateDirectory(directory);

        var path = Path.Combine(directory, "shift.config");
        var json = JsonSerializer.Serialize(this);
        File.WriteAllText(path, json);
    }

    public static ShiftOptions LoadUserDefault()
    {
        ShiftOptions? options;
        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "shift", "shift.config");
        if (!File.Exists(path))
        {
            options = new ShiftOptions();
            options.SaveUserDefault();
        }
        else
        {
            using var stream = File.OpenRead(path);
            options = JsonSerializer.Deserialize<ShiftOptions>(stream);
            if (options is null)
            {
                options = new ShiftOptions();
                options.SaveUserDefault();
            }
        }

        return options;
    }

    public class StoreSection
    {
        public required string Provider { get; set; }
        public Dictionary<string, string> Args { get; set; } = new(StringComparer.InvariantCultureIgnoreCase);

        public static StoreSection Default => new StoreSection
        {
            Provider = "LiteDb",
            Args =
            {
                ["Path"] = "default.lite",
            }
        };
    }
}
