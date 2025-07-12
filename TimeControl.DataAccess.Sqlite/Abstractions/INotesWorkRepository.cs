using TimeControl.Core.Models;

namespace TimeControl.DataAccess.Sqlite.Abstractions
{
    public interface INotesWorkRepository
    {
        Task<Guid> AddAsync(NotesWork notesWork);
        Task<List<NotesWork>> GetAllAsync();
        Task<List<NotesWork>> GetByDateAsync(DateOnly date);
    }
}
