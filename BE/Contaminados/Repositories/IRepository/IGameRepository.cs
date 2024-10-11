namespace Contaminados.Repositories.IRepository
{
    public interface IGameRepository<T>
    {
        Task CreateGameAsync(T game);
        Task UpdateGameAsync(T game);
        Task DeleteGameAsync(Guid id);
        Task<T> GetGameByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllGamesAsync();
    }
}