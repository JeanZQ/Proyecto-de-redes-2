using Contaminados.Models.Common;

namespace Contaminados.Application.Commands
{

    public class UpdatePlayerCommand
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public string PlayerName { get; set; }
        public bool IsEnemy { get; set; }

        public UpdatePlayerCommand(Guid id, Guid gameId, string playerName, bool isEnemy)
        {
            Id = id;
            GameId = gameId;
            PlayerName = playerName;
            IsEnemy = isEnemy;
        }
    }

}