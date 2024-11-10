using Contaminados.Models.Common;

namespace Contaminados.Application.Commands

{

    public class UpdateGameCommand
    {
        public Guid Id { get; set; }
        public Status GameStatus { get; set; }
        public Guid CurrentRoundId { get; set; }
        public string Player { get; set; }
        public string? Password { get; set; }

        public UpdateGameCommand(Guid id, Status gameStatus, Guid currentRoundId, string player, string password)
        {
            Id = id;
            GameStatus = gameStatus;
            CurrentRoundId = currentRoundId;
            Player = player;
            Password = password;
        }

    }
}
