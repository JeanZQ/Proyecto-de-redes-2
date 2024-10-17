using Models.playersModels;

namespace Contaminados.Repositories.IRepository
{
    public interface IPlayerRepository<PLayers>
    {
        Task CreatePlayerAsync(Players player);
        Task UpdatePlayerAsync(Players player);
        Task DeletePlayerAsync(Guid id);
        Task<Players> GetPlayerByIdAsync(Guid id);
        Task<IEnumerable<Players>> GetAllPlayerAsync();
        Task<IEnumerable<Players>> GetAllPlayersByGameIdAsync(Guid gameId);
    }
}