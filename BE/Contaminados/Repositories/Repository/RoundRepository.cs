using Contaminados.DB;
using Contaminados.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.roundModels;

namespace Contaminados.Repositories.Repository
{
    public class RoundRepository : IRoundRepository<Round>
    {
        private readonly DbContextClass _context;
        public RoundRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task CreateRoundAsync(Round round)
        {
            await _context.Set<Round>().AddAsync(round);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Round>> GetAllRoundByGameIdAsync(Guid gameId)
        {
            return await _context.Set<Round>().Where(x => x.GameId == gameId).ToListAsync();
        }

        public async Task<Round> GetRoundByIdAsync(Guid id)
        {
            var round = await _context.Set<Round>().FindAsync(id);
            return round;
        }

        public async Task UpdateRoundAsync(Round round)
        {
            _context.Set<Round>().Update(round);
            await _context.SaveChangesAsync();
        }
    }
}