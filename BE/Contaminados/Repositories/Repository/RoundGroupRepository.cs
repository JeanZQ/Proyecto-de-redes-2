using Contaminados.DB;
using Contaminados.Repositories.IRepository;
using Models.roundGroupModels;
using Microsoft.EntityFrameworkCore;

namespace Contaminados.Repositories.Repository
{
    public class RoundGroupRepository : IRoundGroupRepository<RoundGroup>
    {
        private readonly DbContextClass _context;
        public RoundGroupRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task CreateRoundGroupAsync(RoundGroup roundGroup)
        {
            await _context.Set<RoundGroup>().AddAsync(roundGroup);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllRoundGroupsByRoundIdAsync(Guid roundId)
        {
            var roundGroups = _context.Set<RoundGroup>().Where(x => x.RoundId == roundId);
            _context.Set<RoundGroup>().RemoveRange(roundGroups);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoundGroup>> GetAllRoundGroupByRoundIdAsync(Guid roundId)
        {
            return await _context.Set<RoundGroup>().Where(x => x.RoundId == roundId).ToListAsync();
        }
    }
}