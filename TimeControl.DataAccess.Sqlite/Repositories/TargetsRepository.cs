using Microsoft.EntityFrameworkCore;
using TimeControl.Core.Models;
using TimeControl.DataAccess.Sqlite.Abstractions;
using TimeControl.DataAccess.Sqlite.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TimeControl.DataAccess.Sqlite.Repositories
{
    public class TargetsRepository : ITargetsRepository
    {
        public TargetsRepository(TimeControlDbContext context)
        {
            _context = context;
        }

        private readonly TimeControlDbContext _context;

        public async Task<List<Targets>> GetByDateAsync(DateOnly date)
        {
            var result = await _context.Targets
                .AsNoTracking()
                .Where(a => a.DateWork == date)
                .ToListAsync();
            return result.Select(a => Targets.Create(a.Id, a.DateWork, a.Description,
                a.DurationMinutes, a.DoneDurationMinutes, a.IsDone).target).ToList()!;
        }

        public async Task<Guid> AddAsync(Targets target)
        {
            TargetsEntity targetsEntity = new TargetsEntity()
            {
                Id = target.Id,
                DateWork = target.DateWork,
                Description = target.Description,
                DurationMinutes = target.DurationMinutes,
                DoneDurationMinutes = target.DoneDurationMinutes,
                IsDone = target.IsDone
            };
            await _context.Targets.AddAsync(targetsEntity);
            await _context.SaveChangesAsync();
            return target.Id;
        }

        public async Task<int> DeleteByDescriptionAsync(string description)
        {
            return await _context.Targets
                .AsNoTracking()
                .Where(a => a.Description == description)
                .ExecuteDeleteAsync();
        }

        public async Task<bool> IsHaveAsync(string description, DateOnly date)
        {
            var result = await _context.Targets
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.DateWork == date || a.Description == description);
            if (result is null) return false;
            return true;
        }

        public async Task<int> UpdateAsync(Targets target)
        {
            return await _context.Targets
                .AsNoTracking()
                .Where(a => a.DateWork == target.DateWork 
                    || a.Description == target.Description)
                .ExecuteUpdateAsync(s => s
                .SetProperty(s => s.DoneDurationMinutes, target.DoneDurationMinutes)
                .SetProperty(s => s.IsDone, target.IsDone));
        }
    }
}
