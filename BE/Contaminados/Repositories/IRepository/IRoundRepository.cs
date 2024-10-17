namespace Contaminados.Repositories.IRepository
{
    public interface IRoundRepository<Round>
    {
        Task CreateRoundAsync(Round round);
        Task UpdateRoundAsync(Round round);
        Task<Round> GetRoundByIdAsync(Guid id);
        Task<IEnumerable<Round>> GetAllRoundByGameIdAsync(Guid gameId);
    }
}