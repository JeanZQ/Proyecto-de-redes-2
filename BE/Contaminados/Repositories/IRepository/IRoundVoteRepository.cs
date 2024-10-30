namespace Contaminados.Repositories.IRepository
{
    public interface IRoundVoteRepository<RoundVote>
    {
        Task CreateRoundVoteAsync(RoundVote roundVote);
        Task<IEnumerable<RoundVote>> GetAllRoundVoteByRoundIdAsync(Guid roundId);
        Task UpdateRoundVoteAsync(RoundVote roundVote);
        Task<RoundVote> GetRoundVoteByGameIdByPlayerNameAsync(Guid roundId, string playerName);
    }
}