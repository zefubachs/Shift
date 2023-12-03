using Shift.Storage.Models;

namespace Shift.Storage.Repositories;
public interface IProjectRepository
{
    Task<Project?> FindByIdAsync(string id);
    Task<Project?> FindByNameAsync(string name);
    Task<IReadOnlyCollection<Project>> GetAllAsync();
    Task AddAsync(Project project);
    Task<bool> DeleteAsync(string id);
}
