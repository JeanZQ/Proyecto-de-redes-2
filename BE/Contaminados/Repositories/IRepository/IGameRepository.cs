using Models.gameModels;

namespace Contaminados.Repositories.IRepository
{
    public interface IGameRepository<Game>
    {
        Task CreateGameAsync(Game game);
        Task UpdateGameAsync(Game game);
        Task DeleteGameAsync(Guid id);
        Task<Game> GetGameByIdAsync(Guid id);
        Task<IEnumerable<Game>> GetAllGamesAsync();

        Task<IEnumerable<Game>> GetGamesByDate();

    }
}