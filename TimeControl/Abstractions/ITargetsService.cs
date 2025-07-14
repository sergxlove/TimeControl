using TimeControl.Core.Models;

namespace TimeControl.Abstractions
{
    public interface ITargetsService
    {
        Task<Guid> AddAsync(Targets target);
        Task<int> DeleteByDescriptionAsync(string description);
        Task<List<Targets>> GetByDateAsync(DateOnly date);
    }
}
