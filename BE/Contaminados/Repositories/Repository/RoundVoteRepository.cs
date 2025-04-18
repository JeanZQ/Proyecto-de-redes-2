using Contaminados.DB;
using Contaminados.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.roundVoteModels;

namespace Contaminados.Repositories.Repository
{
    public class RoundVoteRepository : IRoundVoteRepository<RoundVote>
    {
        private readonly DbContextClass _context;
        public RoundVoteRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task CreateRoundVoteAsync(RoundVote roundVote)
        {
            await _context.Set<RoundVote>().AddAsync(roundVote);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllRoundVotesByRoundIdAsync(Guid roundId)
        {
            var roundVotes = _context.Set<RoundVote>().Where(x => x.RoundId == roundId);
            _context.Set<RoundVote>().RemoveRange(roundVotes);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoundVote>> GetAllRoundVoteByRoundIdAsync(Guid roundId)
        {
            return await _context.Set<RoundVote>().Where(x => x.RoundId == roundId).ToListAsync();
        }

        public async Task<RoundVote> GetRoundVoteByGameIdByPlayerNameAsync(Guid roundId, string playerName)
        {
            return await _context.Set<RoundVote>().FirstOrDefaultAsync(x => x.RoundId == roundId && x.PlayerName == playerName);
        }

        public async Task UpdateRoundVoteAsync(RoundVote roundVote)
        {
            _context.Set<RoundVote>().Update(roundVote);
            await _context.SaveChangesAsync();
        }
    }
}