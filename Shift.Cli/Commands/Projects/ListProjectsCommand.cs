using Shift.Storage;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Shift.Cli.Commands.Projects;
public class ListProjectsCommand(IAnsiConsole console, IStorageSession session) : AsyncCommand<ListProjectsCommand.Settings>
{
    public class Settings : CommandSettings
    {

    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var projects = await session.Projects.GetAllAsync();
        if (projects.Count == 0)
        {
            console.WriteLine("No projects registered");
        }
        else
        {
            var grid = new Grid();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();

            grid.AddRow("Id", "Name", "Description", "Active");
            foreach (var project in projects)
            {
                grid.AddRow(project.Id, project.Name, project.Description ?? string.Empty, project.Active ? "Yes" : "No");
            }

            console.Write(grid);
        }

        return 0;
    }
}
