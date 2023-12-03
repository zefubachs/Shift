using Shift.Storage;
using Shift.Storage.Models;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Shift.Cli.Commands.Projects;
public class DeleteProjectCommand(IAnsiConsole console, IStorageSession session) : AsyncCommand<DeleteProjectCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<NAME OR ID>")]
        [Description("The name of id of the project to delete.")]
        public required string IdOrName { get; set; }
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var project = await session.Projects.FindByIdAsync(settings.IdOrName);
        if (project is null)
            project = await session.Projects.FindByNameAsync(settings.IdOrName);

        if (project is null)
        {
            console.WriteLine($"Project '{settings.IdOrName}' not found.");
            return 1;
        }

        var success = await session.Projects.DeleteAsync(project.Id);
        if (success)
        {
            console.WriteLine($"Project '{project.Name}' [{project.Id}] has been deleted");
            return 0;
        }
        else
        {
            console.WriteLine($"Failed to delete project '{project.Name}' [{project.Id}].");
            return 2;
        }
    }
}
