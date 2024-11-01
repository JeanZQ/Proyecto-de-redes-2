namespace Contaminados.Repositories.IRepository
{
    public interface IRoundGroupRepository<RoundGroup>
    {
        Task CreateRoundGroupAsync(RoundGroup roundGroup);
        Task<IEnumerable<RoundGroup>> GetAllRoundGroupByRoundIdAsync(Guid roundId);
        Task DeleteAllRoundGroupsByRoundIdAsync(Guid roundId);
    }
}