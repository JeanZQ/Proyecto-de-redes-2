namespace Contaminados.Application.Queries
{
    public class GetAllRoundGroupByRoundIdQuery
    {
        public Guid RoundId { get; }
        public GetAllRoundGroupByRoundIdQuery(Guid roundId)
        {
            RoundId = roundId;
        }
    }
}