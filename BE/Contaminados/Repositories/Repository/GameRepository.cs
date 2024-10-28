using Contaminados.DB;
using Contaminados.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using Models.gameModels;

namespace Contaminados.Repositories.Repository
{
    public class GameRepository : IGameRepository<Game>
    {
        private readonly DbContextClass _context;
        public GameRepository(DbContextClass context)
        {
            _context = context;
        }

        public async Task CreateGameAsync(Game game)
        {
            await _context.Set<Game>().AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(Guid id)
        {
            var game = await _context.Set<Game>().FindAsync(id);
            _context.Set<Game>().Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Set<Game>().ToListAsync();
        }

        public async Task<Game> GetGameByIdAsync(Guid id)
        {
            var game = await _context.Set<Game>().FindAsync(id);
            return game;
        }

        public async Task UpdateGameAsync(Game game)
        {
            _context.Set<Game>().Update(game);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByDate()
        {
            return await _context.Set<Game>().OrderByDescending(g => g.CreatedAt).ToListAsync();
        }

    }
}