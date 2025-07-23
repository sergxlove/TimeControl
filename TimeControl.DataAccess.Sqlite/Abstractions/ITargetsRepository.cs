using TimeControl.Core.Models;

namespace TimeControl.DataAccess.Sqlite.Abstractions
{
    public interface ITargetsRepository
    {
        Task<Guid> AddAsync(Targets target);
        Task<int> DeleteByDescriptionAsync(string description);
        Task<List<Targets>> GetByDateAsync(DateOnly date);
        Task<bool> IsHaveAsync(string description, DateOnly date);
        Task<int> UpdateAsync(Targets target);
    }
}
