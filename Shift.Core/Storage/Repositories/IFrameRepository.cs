using Shift.Storage.Models;

namespace Shift.Storage.Repositories;
public interface IFrameRepository
{
    Task<Frame?> FindByIdAsync(string id);
    Task<Frame?> FindActiveAsync();
    Task AddAsync(Frame frame);
    Task<bool> SetEndAsync(string id, DateTime dateTime);
}
