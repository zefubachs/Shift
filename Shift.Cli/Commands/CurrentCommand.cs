using Shift.Storage;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shift.Cli.Commands;
public class CurrentCommand(IAnsiConsole console, IStorageSession session) : AsyncCommand<CurrentCommand.Settings>
{
    public class Settings : CommandSettings
    {

    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var frame = await session.Frames.FindActiveAsync();
        if (frame is null)
        {
            console.WriteLine("No active frame");
            return 0;
        }

        var project = await session.Projects.FindByIdAsync(frame.Project);
        if (project is null)
        {
            console.WriteLine($"Orphraned frame [{frame.Id}]");
            return 1;
        }

        var grid = new Grid();
        grid.AddColumn();
        grid.AddColumn();

        grid.AddRow("[bold]Project[/]", $"{project.Name} [[{project.Id}]]");
        grid.AddRow("[bold]Started[/]", $"{frame.Start:dd/MM/yyyy HH:mm}");
        grid.AddRow("[bold]Tags[/]", frame.Tags.Select(x => $"[[{x}]]").Aggregate((previous, current) => previous + current));

        console.Write(grid);
        return 0;
    }
}
