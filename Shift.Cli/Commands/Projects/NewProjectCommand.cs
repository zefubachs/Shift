using Shift.Storage;
using Shift.Storage.Models;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Shift.Cli.Commands.Projects;
public class NewProjectCommand(IAnsiConsole console, IStorageSession session) : AsyncCommand<NewProjectCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<NAME>")]
        [Description("The name for the new project, must be unique.")]
        public required string Name { get; set; }
        [CommandOption("-d|--description")]
        [Description("A longer description for the new project.")]
        public string? Description { get; set; }
        [CommandOption("--disabled")]
        [DefaultValue(false)]
        [Description("Should this new project be disabled.")]
        public bool Disabled { get; set; } = false;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var project = new Project
        {
            Name = settings.Name,
            Description = settings.Description,
            Active = !settings.Disabled,
        };
        await session.Projects.AddAsync(project);
        console.WriteLine($"Project '{project.Name}' [{project.Id}] is created");
        return 0;
    }
}
