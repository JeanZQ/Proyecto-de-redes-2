namespace Contaminados.Aplication.Queries
{
    public class GetPlayersByGameIdQuery
    {
        public Guid GameId { get; }
        public GetPlayersByGameIdQuery(Guid gameId)
        {
            GameId = gameId;
        }
    }
}