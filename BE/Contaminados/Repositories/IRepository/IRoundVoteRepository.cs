namespace Contaminados.Repositories.IRepository
{
    public interface IRoundVoteRepository<RoundVote>
    {
        Task CreateRoundVoteAsync(RoundVote roundVote);
        Task<IEnumerable<RoundVote>> GetAllRoundVoteByRoundIdAsync(Guid roundId);
    }
}