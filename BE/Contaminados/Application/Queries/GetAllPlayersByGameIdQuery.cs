namespace Contaminados.Application.Queries
{
    public class GetAllPlayersByGameIdQuery
    {
        public Guid GameId { get; }
        public GetAllPlayersByGameIdQuery(Guid gameId)
        {
            GameId = gameId;
        }
    }
}