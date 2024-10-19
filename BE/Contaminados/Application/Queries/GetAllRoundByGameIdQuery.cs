namespace Contaminados.Application.Queries
{
    public class GetAllRoundByGameIdQuery
    {
        public Guid GameId { get; }
        public GetAllRoundByGameIdQuery(Guid gameId)
        {
            GameId = gameId;
        }
    }
}