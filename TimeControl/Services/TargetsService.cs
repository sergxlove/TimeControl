using TimeControl.Abstractions;
using TimeControl.Core.Models;
using TimeControl.DataAccess.Sqlite.Abstractions;

namespace TimeControl.Services
{
    public class TargetsService : ITargetsService
    {
        public TargetsService(ITargetsRepository targetsRepository)
        {
            _repository = targetsRepository;
        }

        private readonly ITargetsRepository _repository;

        public async Task<List<Targets>> GetByDateAsync(DateOnly date)
        {
            return await _repository.GetByDateAsync(date);
        }

        public async Task<Guid> AddAsync(Targets target)
        {
            return await _repository.AddAsync(target);
        }

        public async Task<int> DeleteByDescriptionAsync(string description)
        {
            return await _repository.DeleteByDescriptionAsync(description);
        }

        public async Task<bool> IsHaveAsync(string description, DateOnly date)
        {
            return await _repository.IsHaveAsync(description, date);
        }

        public async Task<int> UpdateAsync(Targets target)
        {
            return await _repository.UpdateAsync(target);
        }
    }
}
