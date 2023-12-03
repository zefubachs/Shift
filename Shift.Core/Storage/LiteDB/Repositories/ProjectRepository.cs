using LiteDB;
using Shift.Storage.LiteDB.Entities;
using Shift.Storage.Models;
using Shift.Storage.Repositories;

namespace Shift.Storage.LiteDB.Repositories;
public class ProjectRepository : IProjectRepository
{
    private const string COLLECTION_NAME = "projects";

    private readonly ILiteCollection<ProjectEntity> collection;

    public ProjectRepository(LiteDatabase database)
    {
        collection = database.GetCollection<ProjectEntity>(COLLECTION_NAME);
        collection.EnsureIndex("Name", "LOWER($.Name)", unique: true);
    }

    public Task<Project?> FindByIdAsync(string id)
    {
        var entity = collection.FindById(new ObjectId(id));
        if (entity is null)
            return Task.FromResult<Project?>(null);

        var project = new Project
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Description = entity.Description,
            Active = entity.Active,
        };
        return Task.FromResult<Project?>(project);
    }

    public Task<Project?> FindByNameAsync(string name)
    {
        var entity = collection.FindOne(x => x.Name == name);
        if (entity is null)
            return Task.FromResult<Project?>(null);

        var project = new Project
        {
            Id = entity.Id.ToString(),
            Name = entity.Name,
            Description = entity.Description,
            Active = entity.Active,
        };
        return Task.FromResult<Project?>(project);
    }

    public Task<IReadOnlyCollection<Project>> GetAllAsync()
    {
        var entities = collection.FindAll();
        var projects = entities.Select(x => new Project
        {
            Id = x.Id.ToString(),
            Name = x.Name,
            Description = x.Description,
            Active = x.Active,
        }).ToList();
        return Task.FromResult<IReadOnlyCollection<Project>>(projects);
    }

    public Task AddAsync(Project project)
    {
        var entity = new ProjectEntity
        {
            Name = project.Name,
            Description = project.Description,
            Active = project.Active,
        };
        collection.Insert(entity);
        project.Id = entity.Id.ToString();
        return Task.CompletedTask;
    }

    public Task<bool> DeleteAsync(string id)
    {
        var result = collection.Delete(new ObjectId(id));
        return Task.FromResult(result);
    }
}
