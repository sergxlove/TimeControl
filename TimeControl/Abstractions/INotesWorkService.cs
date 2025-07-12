using TimeControl.Core.Models;

namespace TimeControl.Abstractions
{
    public interface INotesWorkService
    {
        Task<Guid> AddAsync(NotesWork notesWork);
        Task<List<NotesWork>> GetAllAsync();
        Task<List<NotesWork>> GetByDataAsync(DateOnly date);
    }
}
