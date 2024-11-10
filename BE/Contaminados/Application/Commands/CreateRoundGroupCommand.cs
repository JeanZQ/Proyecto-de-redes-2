using Application.Commands.Common;
using Models.gameModels;
using Models.roundModels;

namespace Contaminados.Application.Commands
{
    public class CreateRoundGroupCommand
    {
        public Guid RoundId { get; set; }
        public IEnumerable<string> Players { get; set; }
        public string Leader { get; set; }
        public Guid GameId { get; set; }
        public CreateRoundGroupCommand(Guid roundId, IEnumerable<string> players, string leader, Guid gameId)
        {
            RoundId = roundId;
            Players = players;
            Leader = leader;
            GameId = gameId;
        }
    }
}