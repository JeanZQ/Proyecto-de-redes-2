namespace Contaminados.Application.Queries
{
    public class GetRoundVoteByGameIdByPlayerNameQuery
    {
        public Guid GameId { get; }
        public string PlayerName { get; }
        public GetRoundVoteByGameIdByPlayerNameQuery(Guid gameId, string playerName)
        {
            GameId = gameId;
            PlayerName = playerName;
        }
    }

}