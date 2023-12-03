using Shift.Storage;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shift.Cli.Commands;
public class StopCommand(IAnsiConsole console, IStorageSession session) : AsyncCommand<StopCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandOption("--at")]
        public TimeSpan? Time { get; set; }

        public DateTime NormalizedTime
        {
            get
            {
                if (Time is null)
                    return DateTime.Now;

                return DateTime.Now.Date + Time.Value;
            }
        }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var frame = await session.Frames.FindActiveAsync();
        if (frame is null)
        {
            console.WriteLine("No active frame");
            return 1;
        }

        var normalizedDate = settings.NormalizedTime;
        await session.Frames.SetEndAsync(frame.Id, normalizedDate);
        console.WriteLine($"Stopped frame [{frame.Id}] at {normalizedDate}");
        return 0;
    }
}
