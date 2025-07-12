using Microsoft.EntityFrameworkCore;
using TimeControl.Core.Models;
using TimeControl.DataAccess.Sqlite.Abstractions;
using TimeControl.DataAccess.Sqlite.Models;

namespace TimeControl.DataAccess.Sqlite.Repositories
{ 
    public class NotesWorkRepository : INotesWorkRepository
    {
        public NotesWorkRepository(TimeControlDbContext context)
        {
            _context = context;
        }

        private readonly TimeControlDbContext _context;

        public async Task<List<NotesWork>> GetAllAsync()
        {
            var result = await _context.NotesWork
                .AsNoTracking()
                .ToListAsync();

            return result.Select(a => NotesWork.Create(a.Id, a.DateWork,
                a.Description, a.DurationHour, a.DurationMinute).notesWork!).ToList();

        }

        public async Task<Guid> AddAsync(NotesWork notesWork)
        {
            NotesWorkEntity notesWorkEntity = new NotesWorkEntity()
            {
                Id = notesWork.Id,
                DateWork = notesWork.DateWork,
                Description = notesWork.Description,
                DurationHour = notesWork.DurationHour,
                DurationMinute = notesWork.DurationMinute,
            };

            await _context.NotesWork.AddAsync(notesWorkEntity);
            await _context.SaveChangesAsync();
            return notesWorkEntity.Id;
        }

        public async Task<List<NotesWork>> GetByDateAsync(DateOnly date)
        {
            var result = await _context.NotesWork
                .Where(a => a.DateWork == date)
                .ToListAsync();

            return result.Select(a => NotesWork.Create(a.Id, a.DateWork,
               a.Description, a.DurationHour, a.DurationMinute).notesWork!).ToList();
        }
    }
}
