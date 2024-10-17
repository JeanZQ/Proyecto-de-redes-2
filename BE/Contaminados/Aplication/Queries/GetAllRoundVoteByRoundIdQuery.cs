namespace Contaminados.Aplication.Queries
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