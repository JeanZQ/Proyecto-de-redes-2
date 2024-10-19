namespace Contaminados.Application.Queries
{
    public class GetAllRoundVoteByRoundIdQuery
    {
        public Guid RoundId { get; }
        public GetAllRoundVoteByRoundIdQuery(Guid roundId)
        {
            RoundId = roundId;
        }
    }
}