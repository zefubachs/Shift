using Microsoft.Extensions.DependencyInjection;
using Shift.Cli.Commands;
using Shift.Cli.Commands.Projects;
using Shift.Cli.Infrastructure;
using Spectre.Console.Cli;

var services = new ServiceCollection();
services.AddShift();


var registrar = new TypeRegistrar(services);

var app = new CommandApp(registrar);
app.Configure(options =>
{
    options.SetApplicationName("shift.exe");

    options.AddCommand<StartCommand>("start")
    .WithDescription("Start working on a new project")
    .WithExample("start", "MyProject")
    .WithExample("start", "MyProject", "-t", "12:00");


    options.AddCommand<StopCommand>("stop")
    .WithDescription("Stop working on the current project")
    .WithExample("stop")
    .WithExample("stop", "-t", "12:00");

    options.AddCommand<CurrentCommand>("active")
    .WithDescription("Gets the current active frame, if available");

    options.AddBranch("projects", projects =>
    {
        projects.AddCommand<ListProjectsCommand>("list")
        .WithDescription("List all projects");

        projects.AddCommand<NewProjectCommand>("add")
        .WithDescription("Add a new project");

        projects.AddCommand<DeleteProjectCommand>("delete")
        .WithDescription("Delete a project");
    });

    //options.AddBranch("profiles", profiles =>
    //{

    //});
});
await app.RunAsync(args);