namespace Contaminados.Application.Queries
{
    public class GetGameByIdByPasswordByPlayerQuery
    {
        public Guid Id { get; }
        public string? Password { get; }
        public string Player { get; }
        public GetGameByIdByPasswordByPlayerQuery(Guid id, string? password, string player)
        {
            Id = id;
            Password = password;
            Player = player;
        }
    }
}