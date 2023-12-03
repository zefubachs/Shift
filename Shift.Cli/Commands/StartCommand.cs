using Shift.Storage;
using Shift.Storage.Models;
using Spectre.Console;
using Spectre.Console.Cli;
using System.ComponentModel;

namespace Shift.Cli.Commands;

public class StartCommand(IAnsiConsole console, IStorageSession session) : AsyncCommand<StartCommand.Settings>
{
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "<Project>")]
        public required string Project { get; set; }
        [CommandOption("--at")]
        [Description("Start time of the work")]
        public TimeSpan? Time { get; set; }
        [CommandOption("-t|--tag <TAGS>")]
        [Description("Tags to add to the starting work")]
        public string[]? Tags { get; set; }
        [CommandOption("-n|--new")]
        [Description("Create project if it does not exist")]
        [DefaultValue(false)]
        public bool CreateProject { get; set; }
        [CommandOption("-s|--stop-active-frame")]
        [Description("Stop command if there is an active frame.")]
        [DefaultValue(false)]
        public bool StopActiveFrame { get; set; }

        public DateTime NormalizeDate
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
        var project = await session.Projects.FindByIdAsync(settings.Project);
        if (project is null)
            project = await session.Projects.FindByNameAsync(settings.Project);

        if (project is null)
        {
            if (!settings.CreateProject)
            {
                console.WriteLine($"Project '{settings.Project}' does not exist.");
                return 1;
            }

            project = new Project { Name = settings.Project };
            await session.Projects.AddAsync(project);
            console.WriteLine($"Created project '{project.Name}' [{project.Id}].");
        }

        var startedDate = settings.NormalizeDate;
        var active = await session.Frames.FindActiveAsync();
        if (active is not null)
        {
            if (settings.StopActiveFrame)
            {
                console.WriteLine($"Current active frame [{active.Id}].");
                return 2;
            }

            await session.Frames.SetEndAsync(active.Id, startedDate);
            console.WriteLine($"Stopped frame [{active.Id}]");
        }

        var frame = new Frame
        {
            Project = project.Id,
            Start = startedDate,
            Tags = settings.Tags?.ToHashSet() ?? [],
        };
        await session.Frames.AddAsync(frame);
        console.WriteLine($"Started new frame [{frame.Id}] at {frame.Start:HH:mm:ss}.");
        return 0;
    }
}
