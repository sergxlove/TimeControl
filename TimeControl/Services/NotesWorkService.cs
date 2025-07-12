using TimeControl.Abstractions;
using TimeControl.Core.Models;
using TimeControl.DataAccess.Sqlite.Abstractions;

namespace TimeControl.Services
{
    public class NotesWorkService : INotesWorkService
    {
        public NotesWorkService(INotesWorkRepository repository)
        {
            _repository = repository;
        }
        private readonly INotesWorkRepository _repository;

        public async Task<List<NotesWork>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Guid> AddAsync(NotesWork notesWork)
        {
            return await _repository.AddAsync(notesWork);
        }

        public async Task<List<NotesWork>> GetByDataAsync(DateOnly date)
        {
            return await _repository.GetByDateAsync(date);
        }
    }
}
