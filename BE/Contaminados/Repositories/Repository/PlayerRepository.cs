using Contaminados.DB;
using Contaminados.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.playersModels;

namespace Contaminados.Repositories.Repository
{
    public class PlayerRepository : IPlayerRepository<Players>
    {
        private readonly DbContextClass _context;
        public PlayerRepository(DbContextClass context)
        {
            _context = context;
        }
        public async Task CreatePlayerAsync(Players player)
        {
            await _context.Set<Players>().AddAsync(player);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePlayerAsync(Guid id)
        {
            var player = await _context.Set<Players>().FindAsync(id) ?? throw new KeyNotFoundException($"Player with id {id} not found.");
            _context.Set<Players>().Remove(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Players>> GetAllPlayerAsync()
        {
            return await _context.Set<Players>().ToListAsync();
        }

        public async Task<Players> GetPlayerByIdAsync(Guid id)
        {
            var player = await _context.Set<Players>().FindAsync(id) ?? throw new KeyNotFoundException($"Player with id {id} not found.");
            return player;
        }

        public async Task UpdatePlayerAsync(Players player)
        {
            _context.Set<Players>().Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Players>> GetAllPlayersByGameIdAsync(Guid gameId)
        {
            return await _context.Set<Players>().AsNoTracking().Where(x => x.GameId == gameId).ToListAsync();
        }
    }
}