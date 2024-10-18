namespace Contaminados.Application.Commands

{

    public class UpdateGameCommand
    {
        public Guid GameId { get; set; }
        public string Password { get; set; }
        public string PlayerName { get; set; }

        public UpdateGameCommand(Guid gameId, string password, string playerName)
        {
            GameId = gameId;
            Password = password;
            PlayerName = playerName;
        }
    }
}
