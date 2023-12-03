using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shift.Storage.LiteDB;
public class LiteDBProvider : IStorageProvider
{
    public string Name => "LiteDB";

    public IStorageSession Create(Dictionary<string, string> arguments)
    {
        if (!arguments.TryGetValue("Path", out var path))
            throw new InvalidOperationException("Missing path argument");

        if (!Path.IsPathRooted(path))
            path = Path.Combine("%appdata%", "shift", path);

        var fullPath = Environment.ExpandEnvironmentVariables(path);
        var database = new LiteDatabase(fullPath);
        return new LiteDBSession(database);
    }
}
